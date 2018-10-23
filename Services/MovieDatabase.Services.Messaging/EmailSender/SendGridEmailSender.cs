namespace MovieDatabase.Services.Messaging.EmailSender
{
    using System;
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
        private readonly HttpClient httpClient;
        private readonly ILogger logger;

        public SendGridEmailSender(ILoggerFactory loggerFactory, IOptions<EmailSettings> settings)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            this.logger = loggerFactory.CreateLogger<SendGridEmailSender>();
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(SendGridEmailSender.AuthenticationScheme, settings.Value.ApiKey);
            this.httpClient.BaseAddress = new Uri(settings.Value.ApiAdress);
            this.fromAddress = settings.Value.FromAddress;
            this.fromName = settings.Value.FromName;
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

            var msg = new SendGridMessage(
                new SendGridEmail(email),
                subject,
                new SendGridEmail(this.fromAddress, this.fromName),
                message);
            try
            {
                var json = JsonConvert.SerializeObject(msg);
                var response = await this.httpClient.PostAsync(
                    SendGridEmailSender.SendEmailUrlPath,
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    // See if we can read the response for more information, then log the error
                    var errorJson = await response.Content.ReadAsStringAsync();
                    throw new Exception($"SendGrid indicated failure! Code: {response.StatusCode}, reason: {errorJson}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception during sending email: {ex}");
            }
        }
    }
}