using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ReadTrack.Shared.Infrastructure.Services
{
    /// <summary>
    /// Configuration options for the SMTP Email Service.
    /// </summary>
    public class SmtpEmailOptions
    {
        public const string SectionName = "SmtpSettings";

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
    }

    /// <summary>
    /// Implementation of IEmailService using standard SMTP protocol.
    /// Uses System.Net.Mail for dependency minimalism in shared infrastructure, 
    /// wrapped with robust error handling and logging.
    /// </summary>
    public class SmtpEmailService : IEmailService, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpEmailOptions _options;
        private readonly ILogger<SmtpEmailService> _logger;
        private bool _disposed;

        public SmtpEmailService(
            IOptions<SmtpEmailOptions> options,
            ILogger<SmtpEmailService> logger)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            ValidateOptions(_options);

            _smtpClient = new SmtpClient(_options.Host, _options.Port)
            {
                Credentials = new NetworkCredential(_options.Username, _options.Password),
                EnableSsl = _options.EnableSsl
            };
        }

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(to)) throw new ArgumentException("To address cannot be empty", nameof(to));
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentException("Subject cannot be empty", nameof(subject));
            if (string.IsNullOrWhiteSpace(body)) throw new ArgumentException("Body cannot be empty", nameof(body));

            try
            {
                _logger.LogInformation("Attempting to send email to {To} with subject {Subject}", to, subject);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_options.FromEmail, _options.FromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await _smtpClient.SendMailAsync(mailMessage, cancellationToken);

                _logger.LogInformation("Successfully sent email to {To}", to);
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "SMTP error occurred while sending email to {To}", to);
                throw new InvalidOperationException("Failed to send email via SMTP.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while sending email to {To}", to);
                throw;
            }
        }

        private void ValidateOptions(SmtpEmailOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Host))
                throw new InvalidOperationException("SMTP Host is not configured.");
            
            if (string.IsNullOrWhiteSpace(options.FromEmail))
                throw new InvalidOperationException("SMTP FromEmail is not configured.");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _smtpClient?.Dispose();
            }

            _disposed = true;
        }
    }
}