using System.Threading.Tasks;

namespace BeautySalonWebApplication.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string subject, string callbackUrl);
    }
}
