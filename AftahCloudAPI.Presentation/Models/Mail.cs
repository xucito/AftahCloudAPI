using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AftahCloudAPI.Presentation.Models
{
    public class Mail
    {
        public string FromAddress { get; set; }
        public string DisplayName { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
