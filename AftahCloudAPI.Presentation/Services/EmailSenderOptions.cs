using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AftahCloudAPI.Presentation.Services
{
    public class EmailSenderOptions
    {
        public string Url { get; set; }
        public string DisplayName { get; set; }
        public string FromAddress { get; set; }
    }
}
