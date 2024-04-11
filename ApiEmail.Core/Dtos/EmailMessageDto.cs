using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmail.Core.Dtos
{
    public class EmailMessageDto
    {
        public EmailMessageDto()
        {
            ToAddresses = new List<EmailAddressDto>();
            FromAddresses = new List<EmailAddressDto>();
        }

        public List<EmailAddressDto> ToAddresses { get; set; }
        public List<EmailAddressDto> FromAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
