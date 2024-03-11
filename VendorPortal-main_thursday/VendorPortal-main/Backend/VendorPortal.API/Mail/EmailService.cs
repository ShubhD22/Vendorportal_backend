using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;

namespace VendorPortal.API.Mail
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value ?? throw new ArgumentNullException(nameof(smtpSettings));
        }

        public void SendEmail(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpSettings.Username)); // Use the configured username
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.TextBody = body;
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpSettings.Server, 465, true); // Connect using port 465 with SSL
                client.Authenticate(_smtpSettings.Username, _smtpSettings.Password); // Use the configured password
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
