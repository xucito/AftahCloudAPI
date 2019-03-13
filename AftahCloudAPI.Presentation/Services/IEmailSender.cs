using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDPCIdentityServer.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toAddress, string subject, string message);
    }
}
