using BeautySalonWebApplication.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace BeautySalonWebApplication.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpEmailSender _smtpEmailSender;
        private readonly ILogger<EmailService> _logger;

        public EmailService(SmtpEmailSender smtpEmailSender, ILogger<EmailService> logger)
        {
            _smtpEmailSender = smtpEmailSender;
            _logger = logger;
        }

        public async Task SendConfirmationEmailAsync(string email, string subject, string confirmationLink)
        {
            try
            {
                await _smtpEmailSender.SendEmailAsync(email, subject, $"Please confirm your email address by clicking the following link: <a href='{confirmationLink}'>Confirm Email</a>");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending confirmation email");
                throw; // Optionally, rethrow the exception
            }
        }
    }
}
