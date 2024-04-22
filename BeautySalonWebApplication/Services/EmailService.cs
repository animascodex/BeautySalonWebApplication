using Microsoft.AspNetCore.Hosting;

namespace BeautySalonWebApplication.Services
{
    public class EmailService : IEmailService
    {
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly ISmtpEmailSender _smtpEmailSender;
        private readonly ILogger<EmailService> _logger;
		public EmailService(IWebHostEnvironment webHostEnvironment, ISmtpEmailSender smtpEmailSender, ILogger<EmailService> logger)
        {
			_webHostEnvironment = webHostEnvironment;
			_smtpEmailSender = smtpEmailSender;
            _logger = logger;
        }
		
		public async Task SendConfirmationEmailAsync(string email, string subject, string confirmationLink, string firstName)
        {
			// Read the template content from the file
			string emailTemplatePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Areas", "Identity", "Pages", "Account", "EmailConfirmation.cshtml");
			string emailBody = await File.ReadAllTextAsync(emailTemplatePath);
			emailBody = emailBody.Replace("@Model.FirstName", firstName);
			emailBody = emailBody.Replace("@Model.ConfirmationLink", confirmationLink);

			_logger.LogInformation("EmailService instantiated.");
            _logger.LogInformation($"SmtpEmailSender: {_smtpEmailSender}");
			_logger.LogInformation(emailBody);
			try
            {
                await _smtpEmailSender.SendEmailAsync(email, subject, emailBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending confirmation email");
                throw; // Optionally, rethrow the exception
            }
        }

	/*	public async Task SendConfirmationEmailAsync(string email, string subject, string confirmationLink, string firstName, string userId, string confirmationCode)
		{
			// Read the template content from the file
			string emailTemplatePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Areas", "Identity", "Pages", "Account", "EmailConfirmation.cshtml");
			string emailBody = await File.ReadAllTextAsync(emailTemplatePath);
			emailBody = emailBody.Replace("@Model.FirstName", firstName);
			emailBody = emailBody.Replace("@Model.UserId", userId);
			emailBody = emailBody.Replace("@Model.ConfirmationCode", confirmationCode);

			_logger.LogInformation("EmailService instantiated.");
			_logger.LogInformation($"SmtpEmailSender: {_smtpEmailSender}");
			_logger.LogInformation(emailBody);
			try
			{
				await _smtpEmailSender.SendEmailAsync(email, subject, emailBody);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error sending confirmation email");
				throw; // Optionally, rethrow the exception
			}
		}*/
	}
}


/*namespace BeautySalonWebApplication.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpEmailSender _smtpEmailSender;
        private readonly ILogger<EmailService> _logger;

        public EmailService(SmtpEmailSender smtpEmailSender, ILogger<EmailService> logger)
        {
            _smtpEmailSender = smtpEmailSender;
            _logger = logger;

            _logger.LogInformation("EmailService instantiated.");
            _logger.LogInformation($"SmtpEmailSender: {_smtpEmailSender}");
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
*/