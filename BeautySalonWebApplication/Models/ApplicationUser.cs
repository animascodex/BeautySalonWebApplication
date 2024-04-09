using Microsoft.AspNetCore.Identity;

namespace BeautySalonWebApplication.Models
{
    // Create a custom user class that inherits from IdentityUser
    public class ApplicationUser : IdentityUser
    {
        // Add any additional properties or customizations here
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Add more properties as needed
    }
}