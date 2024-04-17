using EASendMail;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BeautySalonWebApplication.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace BeautySalonWebApplication.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptions<SmtpSettings> smtpSettings, ILogger<SmtpEmailSender> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                SmtpMail message = new SmtpMail("TryIt");
                SmtpServer oServer = new SmtpServer(_smtpSettings.Host);

                // Set SMTP port
                oServer.Port = _smtpSettings.Port;

                // Set SSL/TLS connection
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                // Set user authentication
                oServer.User = _smtpSettings.Username;
                oServer.Password = _smtpSettings.Password;

                // Set From, To, Subject and TextBody properties
                message.From = _smtpSettings.SenderEmail;
                message.To = email;
                message.Subject = subject;
                message.TextBody = htmlMessage;

                // Send email
                SmtpClient smtpClient = new SmtpClient();
                await smtpClient.SendMailAsync(oServer,message);

                _logger.LogInformation("Email sent successfully to: {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to: {Email}", email);
                throw; // Rethrow the exception to propagate it upwards
            }
        }
    }
}




/*using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BeautySalonWebApplication.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace BeautySalonWebApplication.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptions<SmtpSettings> smtpSettings, ILogger<SmtpEmailSender> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            try
            {
                
                using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
                {
                    // Log SMTP settings
                    _logger.LogInformation("SMTP Settings - Host: {Host}, Port: {Port}, Username: {Username}, SenderEmail: {SenderEmail}",
                       _smtpSettings.Host, _smtpSettings.Port, _smtpSettings.Username, _smtpSettings.SenderEmail);
                    client.EnableSsl = _smtpSettings.EnableSsl;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                    if (string.IsNullOrEmpty(email))
                    {
                        throw new ArgumentNullException(nameof(email));
                    }

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpSettings.SenderEmail),
                        Subject = subject,
                        Body = htmlMessage,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);

                    _logger.LogInformation("Email sent successfully to: {Email}", email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to: {Email}", email);
                throw; // Rethrow the exception to propagate it upwards
            }
        }
    }
}
*/