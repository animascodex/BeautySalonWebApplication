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
            _logger.LogInformation("EmailService instantiated.");
            _logger.LogInformation($"SmtpEmailSender: {_smtpEmailSender}");
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