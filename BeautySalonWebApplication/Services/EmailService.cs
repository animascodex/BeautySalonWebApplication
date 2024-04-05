using BeautySalonWebApplication.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BeautySalonWebApplication.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailService> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string confirmationLink)
        {
            try
            {
                using (var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
                {
                    client.EnableSsl = _smtpSettings.EnableSsl;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpSettings.SenderEmail),
                        Subject = subject,
                        Body = $"Please confirm your email address by clicking the following link: <a href='{confirmationLink}'>Confirm Email</a>",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending confirmation email");
                throw; // Optionally, rethrow the exception
            }
        }
    }
}
