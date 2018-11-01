using EmailAddress = SendGrid.Helpers.Mail.EmailAddress;
using MailHelper = SendGrid.Helpers.Mail.MailHelper;
using SendGridClient = SendGrid.SendGridClient;

namespace MovieDatabase.Services.Messaging.EmailSender
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MovieDatabase.Common.Settings;
    using Newtonsoft.Json;

    // Documentation: https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/index.html
    public class SendGridEmailSender : IEmailSender
    {
        private const string AuthenticationScheme = "Bearer";
        private const string SendEmailUrlPath = "mail/send";

        private readonly string fromAddress;
        private readonly string fromName;
        private readonly SendGridClient sendGridClient;
        private readonly ILogger logger;

        public SendGridEmailSender(ILoggerFactory loggerFactory, IOptions<EmailSettings> settings)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            this.logger = loggerFactory.CreateLogger<SendGridEmailSender>();
            this.fromAddress = settings.Value.FromAddress;
            this.fromName = settings.Value.FromName;
            this.sendGridClient = new SendGridClient(settings.Value.ApiKey);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(this.fromAddress))
            {
                throw new ArgumentOutOfRangeException(nameof(this.fromAddress));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentOutOfRangeException(nameof(email));
            }

            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Subject and/or message must be provided.");
            }

            try
            {
                var from = new EmailAddress(this.fromAddress, this.fromName);
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject,string.Empty, message);
                var response = await this.sendGridClient.SendEmailAsync(msg);
                
                if (response.StatusCode != HttpStatusCode.OK || response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception($"SendGrid indicated failure! Code: {response.StatusCode}, reason: {response.Body}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception during sending email: {ex}");
            }
        }
    }
}