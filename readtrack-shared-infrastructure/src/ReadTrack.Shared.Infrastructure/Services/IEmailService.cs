using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Shared.Infrastructure.Services
{
    /// <summary>
    /// Defines the contract for sending emails via an infrastructure provider (e.g., AWS SES, SMTP).
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body content of the email (HTML supported).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an email asynchronously with a specific template.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="templateName">The name of the email template to use.</param>
        /// <param name="templateData">The data object to hydrate the template.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendTemplatedEmailAsync<TModel>(string to, string templateName, TModel templateData, CancellationToken cancellationToken = default);
    }
}