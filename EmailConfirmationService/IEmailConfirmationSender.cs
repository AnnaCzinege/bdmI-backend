using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailConfirmationService
{
    public interface IEmailConfirmationSender
    {
        Task SendEmailAsync(Message message);
        string CreateEmailContent(string firstName, string confirmationLink);
    }
}
