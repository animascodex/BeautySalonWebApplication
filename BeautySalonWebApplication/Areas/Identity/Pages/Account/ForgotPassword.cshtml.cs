// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using BeautySalonWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using BeautySalonWebApplication.Services;

namespace BeautySalonWebApplication.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailService emailService) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly IEmailService _emailService = emailService;

		[BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await _userManager.FindByEmailAsync(Input.Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return RedirectToPage("./ResetPasswordConfirmation");
			}
			// Generate confirmation token
			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

			// Send confirmation email with token
			var confirmationLink = Url.Page(
				"/Account/ResetPassword",
				pageHandler: null,
				values: new { area = "Identity", userId = user.Id, code = token },
				protocol: Request.Scheme);

			await _emailService.SendConfirmationPasswordResetAsync(Input.Email, "Reset Your Password", confirmationLink, user.FirstName);

			return RedirectToPage("./ResetPasswordConfirmation");
		}
	}
}
