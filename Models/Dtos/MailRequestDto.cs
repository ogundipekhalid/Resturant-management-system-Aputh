using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class MailRequestDto
    {
         public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string AttachmentName { get; set; }
        public string HtmlContent { get; set; }
        public string Subject { get; set; }
    }
}