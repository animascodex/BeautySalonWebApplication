using System.Threading.Tasks;

namespace BeautySalonWebApplication.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string subject, string callbackUrl, string firstName);
        Task SendConfirmationPasswordResetAsync(string email, string subject, string confirmationLink, string firstName);
	}
}
