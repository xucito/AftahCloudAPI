using AftahCloudAPI.Presentation.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EDPCIdentityServer.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public HttpClient client;
        public EmailSenderOptions options;

        public EmailSender(IOptions<EmailSenderOptions> _options)
        {
            client = new HttpClient();
            options = _options.Value;
        }

        public Task SendEmailAsync(string toAddress, string subject, string message )
        {
            return Execute(toAddress, subject, message);
        }

        public Task Execute(string toAddress, string subject, string message)
        {
           return client.PostAsync(options.Url + "/api/mail", new StringContent(
                JsonConvert.SerializeObject(new Mail()
                {
                    FromAddress = options.FromAddress,
                    DisplayName = options.DisplayName,
                    ToAddress = toAddress,
                    Subject = subject,
                    Body = message
                })
                , Encoding.UTF8, "application/json"));
        }
    }
}
