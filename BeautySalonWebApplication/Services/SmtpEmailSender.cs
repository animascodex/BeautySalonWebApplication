using EASendMail;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BeautySalonWebApplication.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using BeautySalonWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

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
		
		public async Task SendEmailAsync(string email, string subject, string template)
		{
			try
			{
				string htmlMessage = template;
				// Resolve the Razor view
				SmtpServer oServer = new SmtpServer(_smtpSettings.Host);
                // Set SMTP port
                oServer.Port = _smtpSettings.Port;
                // Set SSL/TLS connection
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                // Set user authentication
                oServer.User = _smtpSettings.Username;
                oServer.Password = _smtpSettings.Password;
				_logger.LogInformation(_smtpSettings.Username, _smtpSettings.Password, _smtpSettings.SenderEmail, _smtpSettings.Port, _smtpSettings.Host);
				SmtpMail message = new SmtpMail("TryIt");
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