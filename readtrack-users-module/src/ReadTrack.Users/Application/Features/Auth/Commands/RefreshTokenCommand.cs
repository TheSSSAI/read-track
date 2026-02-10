using MediatR;
using ReadTrack.Users.Application.DTOs;

namespace ReadTrack.Users.Application.Features.Auth.Commands
{
    /// <summary>
    /// Command to refresh an expired access token using a valid refresh token.
    /// </summary>
    /// <remarks>
    /// This facilitates the secure session management flow described in Sequence 450.
    /// It requires both the expired access token (for claim extraction) and the refresh token.
    /// </remarks>
    /// <param name="RefreshToken">The long-lived refresh token issued during login.</param>
    /// <param name="AccessToken">The expired access token to be refreshed.</param>
    public record RefreshTokenCommand(string RefreshToken, string AccessToken) : IRequest<AuthResponseDto>;
}