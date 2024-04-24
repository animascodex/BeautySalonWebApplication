using System.ComponentModel.DataAnnotations;
namespace BeautySalonWebApplication.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Email { get; set; }
#pragma warning restore CS8618 

        [Required]
        [DataType(DataType.Password)]
#pragma warning disable CS8618
        public string Password { get; set; }
#pragma warning restore CS8618

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
#pragma warning disable CS8618
        public string ConfirmPassword { get; set; }
#pragma warning restore CS8618

        // Add FirstName and LastName properties
        [Required]
        [Display(Name = "First Name")]
#pragma warning disable CS8618
        public string FirstName { get; set; }
#pragma warning restore CS8618

        [Required]
        [Display(Name = "Last Name")]
#pragma warning disable CS8618 
        public string LastName { get; set; }
#pragma warning restore CS8618

        // Additional properties for registration data
    }
}
