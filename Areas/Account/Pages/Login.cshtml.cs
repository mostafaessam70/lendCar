using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Pages
{
    public class LoginModel : PageModel
    {
        [Required]       
        [BindProperty]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [BindProperty]
        public string Password { get; set; }

        public SignInManager<ApplicationUser> SignInManager { get; }
        public UserManager<ApplicationUser> Usermanger { get; }
       

        public LoginModel(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> usermanger
                         
                         )
        {
            SignInManager = signInManager;
            Usermanger = usermanger;
           

        }
        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return Page();
            else
                return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostAsync()
        {
           

            var result = await SignInManager.PasswordSignInAsync(UserName, Password, false, false);

            if (result.Succeeded)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToPage("Admin", new { area = "Admin"});
                else
                    return RedirectToPage("Index");
            }
            else
                return RedirectToPage("Login", new { area = "Account"});
        }
    }
}