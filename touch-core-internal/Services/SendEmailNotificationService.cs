using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace touch_core_internal.Services
{

    public class SendEmailNotificationService : ISendEmailNotificationService
    {
        public virtual SmtpClient CreateClient()
        {
            var _sender = ConfigurationManager.AppSettings["SenderEmailAddress"];
            var password = ConfigurationManager.AppSettings["SenderEmailPassword"];

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(_sender, password);
                client.EnableSsl = true;
                client.Credentials = credentials;

                return client;
        }

        public async Task SendEmail(EmailNotificationDetails emailNotificationDetails)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailNotificationDetails.SenderAddress);
            message.To.Add(string.Join(",", emailNotificationDetails.RecipientAddresses));
            message.Subject = emailNotificationDetails.SubjectLine;
            message.IsBodyHtml = true; //to make message body as html
            message.Body = emailNotificationDetails.MessageTemplate;
            await emailNotificationDetails.SmtpClient.SendMailAsync(message).ConfigureAwait(false);
        }

    }

    public class EmailNotificationDetails
    {
        public string SenderAddress { get; set; }
        public List<string> RecipientAddresses { get; set; }
        public List<string> CCRecipientAddresses { get; set; }
        public string SubjectLine { get; set; }
        public string MessageTemplate { get; set; }
        public SmtpClient SmtpClient { get; set; }

        public dynamic entity { get; set; }
    }

}
