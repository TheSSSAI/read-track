using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadTrack.Users.Application.DTOs;
using ReadTrack.Users.Application.Interfaces;
using ReadTrack.Users.Domain.Entities;
using ReadTrack.Users.Domain.Enums;

namespace ReadTrack.Users.Application.Features.Auth.Commands
{
    /// <summary>
    /// Handles the coordination of social login requests, validating external tokens,
    /// synchronizing user state, and issuing application authentication tokens.
    /// </summary>
    public class SocialLoginCommandHandler : IRequestHandler<SocialLoginCommand, AuthResponseDto>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IAuth0Service _auth0Service;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogger<SocialLoginCommandHandler> _logger;

        public SocialLoginCommandHandler(
            IUsersDbContext dbContext,
            IAuth0Service auth0Service,
            ITokenGenerator tokenGenerator,
            ILogger<SocialLoginCommandHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _auth0Service = auth0Service ?? throw new ArgumentNullException(nameof(auth0Service));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AuthResponseDto> Handle(SocialLoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing social login for provider: {Provider}", request.Provider);

            // 1. Validate the external ID token with the provider (Auth0)
            // This ensures the token is authentic, not expired, and intended for our audience.
            var externalUserClaims = await _auth0Service.ValidateIdentityTokenAsync(request.IdToken, request.Provider, cancellationToken);

            if (externalUserClaims == null)
            {
                _logger.LogWarning("Social login failed: Invalid identity token from {Provider}", request.Provider);
                throw new UnauthorizedAccessException("Invalid identity token.");
            }

            // 2. Check if the user already exists in our system
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Auth0Id == externalUserClaims.Subject, cancellationToken);

            bool isNewUser = false;

            if (user == null)
            {
                _logger.LogInformation("Creating new user for Auth0Id: {Auth0Id}", externalUserClaims.Subject);
                
                // 3. Register new user
                user = User.Create(
                    externalUserClaims.Subject,
                    externalUserClaims.Email,
                    externalUserClaims.DisplayName ?? "Reader",
                    externalUserClaims.PictureUrl
                );

                await _dbContext.Users.AddAsync(user, cancellationToken);
                isNewUser = true;
            }
            else
            {
                // 4. Update existing user details to keep them in sync with provider
                _logger.LogDebug("Updating existing user: {UserId}", user.Id);
                
                user.UpdateProfile(
                    externalUserClaims.DisplayName ?? user.DisplayName,
                    externalUserClaims.PictureUrl ?? user.ProfilePictureUrl
                );
                
                user.RecordLogin();
                _dbContext.Users.Update(user);
            }

            // 5. Commit changes to database
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 6. Generate Application JWTs
            var accessToken = _tokenGenerator.GenerateAccessToken(user);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            // 7. Store refresh token (Implementation depends on RefreshToken strategy, assumed managed via aggregate or separate service)
            // For simplicity in this handler, we assume token generator handles persistence or it's a stateless reference.
            // If RefreshTokens are an entity, we would add it here.
            await _auth0Service.RevokeOldRefreshTokensAsync(user.Id, cancellationToken); 

            _logger.LogInformation("Social login successful for User: {UserId}. New User: {IsNewUser}", user.Id, isNewUser);

            return new AuthResponseDto(
                accessToken,
                refreshToken,
                user.Id,
                user.Email,
                user.DisplayName,
                user.Role.ToString(),
                isNewUser
            );
        }
    }
}