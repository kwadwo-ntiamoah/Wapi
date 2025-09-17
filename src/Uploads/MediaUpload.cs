using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.Uploads
{
    public class MediaUpload
    {
        public required string File { get; set; }
        public required string Type { get; set; }
        public string MessagingProduct { get; set; } = "whatsapp";
    }
}
