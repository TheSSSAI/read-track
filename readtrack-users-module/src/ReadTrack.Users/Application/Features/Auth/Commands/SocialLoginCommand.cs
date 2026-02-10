using MediatR;
using ReadTrack.Users.Application.DTOs;

namespace ReadTrack.Users.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for authentication responses containing JWT tokens.
    /// </summary>
    public record AuthResponseDto(
        string AccessToken,
        string RefreshToken,
        int ExpiresIn,
        string TokenType = "Bearer"
    );
}

namespace ReadTrack.Users.Application.Features.Auth.Commands
{
    /// <summary>
    /// Command to authenticate a user via a social identity provider (Google/Apple).
    /// </summary>
    /// <remarks>
    /// This command handles the exchange of a third-party ID token for an internal application JWT.
    /// It ensures compliance with REQ-REG-001 by registering new users or logging in existing ones.
    /// </remarks>
    /// <param name="Provider">The social provider name (e.g., "Google", "Apple").</param>
    /// <param name="IdToken">The identity token received from the provider's SDK.</param>
    public record SocialLoginCommand(string Provider, string IdToken) : IRequest<AuthResponseDto>
    {
        /// <summary>
        /// Factory method to create a command from the API request DTO.
        /// </summary>
        /// <param name="request">The social login request DTO.</param>
        /// <returns>A configured SocialLoginCommand.</returns>
        public static SocialLoginCommand FromRequest(SocialLoginRequest request)
        {
            return new SocialLoginCommand(request.Provider, request.IdToken);
        }
    }
}