using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.Extensions
{
    public class WhatsappConfig
    {
        public string AccessToken { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiVersion { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string PhoneNumberId { get; set; } = string.Empty;
        public string BusinessAccountId { get; set; } = string.Empty;
        public string VerificationToken { get; set; } = string.Empty;
    }
}
