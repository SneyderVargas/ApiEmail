﻿using ApiEmail.Core.Dtos;
using ApiEmail.Core.Interfaces;
using ApiEmail.Helpers;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmail.Infra.Repositories
{
    public class SendEmailRepo : ISendEmail
    {
        private readonly EmailConfiguration _emailConfiguration;

        public SendEmailRepo(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public async Task SendAsync(EmailMessageDto Message, bool isHtml, IFormFile file, string StringUbiFile)
        {            
            try
            {
                var message = new MimeMessage();
                message.To.AddRange(Message.ToAddresses.Select(x => new MailboxAddress("name", x.Address)));
                message.From.AddRange(Message.FromAddresses.Select(x => new MailboxAddress("name", x.Address)));

                message.Subject = Message.Subject;
                var builder = new BodyBuilder();
                if (isHtml)
                {
                    builder.HtmlBody = Message.Content;
                }
                else
                {
                    builder.TextBody = Message.Content;
                }
                if (file != null)
                {
                    builder.Attachments.Add(file.FileName, file.OpenReadStream());
                }

                if (StringUbiFile != "")
                {
                    builder.Attachments.Add(@"C:\Downloads\" + StringUbiFile + ".docx");
                }
                //builder.Attachments.Add(@"C:\Users\jsalasma\Downloads\TestReport.pdf");


                message.Body = builder.ToMessageBody();
                using (var emailClient = new SmtpClient())
                {
                    try
                    {
                        //The last parameter here is to use SSL (Which you should!)
                        emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);
                        //Remove any OAuth functionality as we won't be using it. 
                        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                        emailClient.Authenticate(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                        await emailClient.SendAsync(message);
                        await emailClient.DisconnectAsync(true);

                    }
                    catch (Exception e)
                    {
                        var gf = e;
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
