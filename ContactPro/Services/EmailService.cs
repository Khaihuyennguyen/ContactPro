using MailKit.Net.Smtp;
using ContactPro.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace ContactPro.Services
{
    public class EmailService : IEmailSender
        

    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSender = _mailSettings.Email;
            MimeMessage newEmail = new();
            newEmail.Sender = MailboxAddress.Parse(emailSender) ;
            foreach (var emailAddress in email.Split(";"))
            {
                newEmail.To.Add(MailboxAddress.Parse(emailAddress));
            }
            newEmail.Subject = subject;

            BodyBuilder emailBody = new();
            emailBody.HtmlBody = htmlMessage;
            newEmail.Body = emailBody.ToMessageBody();

            //At this point lets log into  smtp clidne
            using SmtpClient smtpClient = new();

            try
            {
                var host = _mailSettings.Host;
                int port = _mailSettings.Port;
                var password = _mailSettings.Password;
                await smtpClient.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(emailSender, password);

                await smtpClient.SendAsync(newEmail);
                await smtpClient.DisconnectAsync(true);


            }
            catch (Exception ex) 
            {
                var error = ex.Message;
                throw;
            }
        }
    }
}
