using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace PizzaWebsite.Services
{
    public class EmailSenderService : IEmailSender
    {
        public readonly IConfiguration Configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = Configuration["SENDGRID_API_KEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(@"pizzawebsitecompany@gmail.com", "Pizza");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, htmlMessage);

            await client.SendEmailAsync(msg);
        }
    }
}
