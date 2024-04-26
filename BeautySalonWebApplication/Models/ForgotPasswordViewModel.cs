using System.ComponentModel.DataAnnotations;

namespace BeautySalonWebApplication.Models
{
    public class ForgotPasswordViewModel
    {
            [Required]
            [EmailAddress]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            public string Email { get; set; }

        
    }
}
