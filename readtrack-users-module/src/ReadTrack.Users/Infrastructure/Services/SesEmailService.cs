using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReadTrack.Users.Application.Interfaces;

namespace ReadTrack.Users.Infrastructure.Services
{
    /// <summary>
    /// Implementation of IEmailService using Amazon SES.
    /// </summary>
    public class SesEmailService : IEmailService
    {
        private readonly IAmazonSimpleEmailService _sesClient;
        private readonly ILogger<SesEmailService> _logger;
        private readonly string _senderAddress;

        public SesEmailService(
            IAmazonSimpleEmailService sesClient,
            IConfiguration configuration,
            ILogger<SesEmailService> logger)
        {
            _sesClient = sesClient ?? throw new ArgumentNullException(nameof(sesClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            _senderAddress = configuration["AWS:SES:SenderEmail"] 
                ?? throw new InvalidOperationException("AWS SES SenderEmail configuration is missing.");
        }

        /// <inheritdoc />
        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("Recipient address cannot be empty.", nameof(to));
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("Email subject cannot be empty.", nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentException("Email body cannot be empty.", nameof(body));
            }

            var sendRequest = new SendEmailRequest
            {
                Source = _senderAddress,
                Destination = new Destination
                {
                    ToAddresses = new List<string> { to }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = body
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = body // Assuming body contains text suitable for fallback or HTML is handled upstream
                        }
                    }
                }
            };

            try
            {
                _logger.LogInformation("Attempting to send email via SES to {Recipient}. Subject: {Subject}", to, subject);

                var response = await _sesClient.SendEmailAsync(sendRequest, cancellationToken);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation("Email sent successfully. MessageId: {MessageId}", response.MessageId);
                }
                else
                {
                    _logger.LogWarning("Failed to send email. SES HTTP Status: {Status}", response.HttpStatusCode);
                    throw new InvalidOperationException($"SES responded with status code {response.HttpStatusCode}");
                }
            }
            catch (MessageRejectedException ex)
            {
                _logger.LogError(ex, "Email rejected by SES. Recipient: {Recipient}", to);
                throw new InvalidOperationException("Email sending failed due to SES rejection.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error sending email via SES to {Recipient}", to);
                throw;
            }
        }
    }
}