using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace LendCar.Pages
{
    public class HomeModel : PageModel
    {
        private SignInManager<ApplicationUser> _signInManager;
        public HomeModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public IActionResult OnGetLogout()
        {
            this._signInManager.SignOutAsync();
            return RedirectToPage("Index");
        }

    }
}