using Microsoft.AspNetCore.Hosting;

namespace BeautySalonWebApplication.Services
{
    public class EmailService(IWebHostEnvironment webHostEnvironment, ISmtpEmailSender smtpEmailSender, ILogger<EmailService> logger) : IEmailService
    {
		private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
		private readonly ISmtpEmailSender _smtpEmailSender = smtpEmailSender;
        private readonly ILogger<EmailService> _logger = logger;

		public async Task SendConfirmationEmailAsync(string email, string subject, string confirmationLink, string firstName)
        {
			// Read the template content from the file
			string emailTemplatePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Areas", "Identity", "Pages", "Account", "EmailConfirmation.cshtml");
			string emailBody = await File.ReadAllTextAsync(emailTemplatePath);
			emailBody = emailBody.Replace("@Model.FirstName", firstName);
			emailBody = emailBody.Replace("@Model.ConfirmationLink", confirmationLink);
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
	}
}