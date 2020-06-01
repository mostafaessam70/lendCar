using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LendCar.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        public SignInManager<ApplicationUser> SignInManager { get; }

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}