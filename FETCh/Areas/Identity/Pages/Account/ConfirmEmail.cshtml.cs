using System;
using System.Text;
using System.Threading.Tasks;
using FETChModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace FETCh.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code, string returnUrl = null)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch
            {
                return BadRequest("Invalid token.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                // Підписуємо користувача (не перезаписуємо persistent cookie)
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Безпечна перевірка returnUrl
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                // За замовчуванням перенаправляємо в Manage (Identity area) або на головну
                return LocalRedirect(Url.Content("~/Identity/Account/Manage") ?? Url.Content("~/"));
            }

            // Якщо не вдалось підтвердити
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }
            return Page();
        }
    }
}
