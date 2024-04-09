namespace BeautySalonWebApplication.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string subject, string confirmationLink);
        // Add other email-related methods if needed
    }
}
