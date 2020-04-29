using MimeKit;
using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace EmailConfirmationService
{
    public class EmailConfirmationSender : IEmailConfirmationSender
    {
        private readonly EmailConfigurationModel _emailConfigurationModel;

        public EmailConfirmationSender(EmailConfigurationModel emailConfigurationModel)
        {
            _emailConfigurationModel = emailConfigurationModel;
        }

        public async Task SendEmailAsync(Message message)
        {
            MimeMessage emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        public string CreateEmailContent(string userName, string confirmationLink)
        {
            return $@"
            Dear {userName},
            As the whole bdmI team, we would like to thank you to register on our website.
            Before you can proceed, you have to confirm your e-mail address that you have provided at the registration.
            Please click on the following link: {confirmationLink}
            If you did not register on the bdmI website, please ignore this letter!
            Best Regards,
            The bdmI team";
        }


        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfigurationModel.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfigurationModel.SmtpServer, _emailConfigurationModel.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfigurationModel.UserName, _emailConfigurationModel.Password);

                await client.SendAsync(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception, or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
