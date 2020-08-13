using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AftahCloudAPI.Presentation.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toAddress, string subject, string message);
    }
}
