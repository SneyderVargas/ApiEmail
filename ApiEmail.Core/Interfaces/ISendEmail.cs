using ApiEmail.Core.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmail.Core.Interfaces
{
    public interface ISendEmail
    {
        Task SendAsync(EmailMessageDto Message, bool isHtml, IFormFile file, string StringUbiFile);
    }
}
