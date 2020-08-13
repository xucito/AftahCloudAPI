using System;

namespace AftahCloudAPI.Presentation.Services
{
    /*
     * dotnet user-secrets set SmtpUser auth_service_account@etisalat.ae
     * dotnet user-secrets set SmtpPassword mysecret
     */
    public class AuthMessageSenderOptions
    {
        public string SmtpEmailFrom { get; set; }

        public string SmtpServerAddress { get; set; }
        public int SmtpServerPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public Boolean SmtpEnableSsl { get; set; }
    }
}
