using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity.IdentityWebAPI.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridAsync(message);
        }

        private async Task ConfigSendGridAsync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            string apiKey = ConfigurationManager.AppSettings["emailService:APIKey"].ToString();
            var client = new SendGridClient(apiKey);

            myMessage.AddTo(message.Destination);            
            myMessage.From = new EmailAddress("taiseer@bitoftech.net", "Taiseer Joudeh");
            myMessage.Subject = message.Subject;
            myMessage.PlainTextContent = message.Body;
            myMessage.HtmlContent = message.Body;

            var msg = MailHelper.CreateSingleEmail(myMessage.From, new EmailAddress("taiseer@bitoftech.net", "Taiseer Joudeh"), myMessage.Subject, myMessage.PlainTextContent, myMessage.HtmlContent);

            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

    }
}