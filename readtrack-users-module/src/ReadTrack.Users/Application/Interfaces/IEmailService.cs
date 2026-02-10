using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Users.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for sending transactional emails (e.g., via AWS SES).
    /// Used for notifications such as Data Export completion or account alerts.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an asynchronous email.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body content of the email (usually HTML).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken);
    }
}