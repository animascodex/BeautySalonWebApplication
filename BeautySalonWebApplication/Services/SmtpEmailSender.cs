using BeautySalonWebApplication.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace BeautySalonWebApplication.Services
{
	public interface ISmtpEmailSender
	{
		Task SendEmailAsync(string email, string subject, string htmlMessage);
	}

    public class SmtpEmailSender : ISmtpEmailSender
	{
		private readonly SmtpClient _smtpClient;
		private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SmtpEmailSender> _logger;

		public SmtpEmailSender(IOptions<SmtpSettings> smtpSettings, ILogger<SmtpEmailSender> logger)
		{
            _smtpSettings = smtpSettings.Value;
			_logger = logger;

			_smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
			{
				Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
				EnableSsl = _smtpSettings.EnableSsl,
				Timeout = 10000
			};
		}
		public async Task SendEmailAsync(string email, string subject, string emailBody)
		{
			try
			{
				string htmlMessage = emailBody;
				// Set From, To, Subject and TextBody properties
				var message = new MailMessage
				{
					From = new MailAddress(_smtpSettings.SenderEmail),
					Subject = subject,
					Body = htmlMessage,
					IsBodyHtml = true
				};
				message.To.Add(new MailAddress(email));
				
				// Send email
				await _smtpClient.SendMailAsync(message);
				
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
