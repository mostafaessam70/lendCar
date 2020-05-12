using System;
using System.Collections.Generic;
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
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public SignInManager<ApplicationUser> SignInManager { get; }
        public UserManager<ApplicationUser> Usermanger { get; }
       
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> usermanger, RoleManager<IdentityRole> roleManager
)
        {
            SignInManager = signInManager;
            Usermanger = usermanger;
            _roleManager = roleManager;

        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            #region AddUserAndRolesAndAssign
            //ApplicationUser user1 = new ApplicationUser()
            //{
            //    Email = "mohamedesam9397@gmail.com",
            //    UserName = "MohamedEsam",
            //    NationalId = "12345678998752",
            //    FirstName = "Mohamed",
            //    Gender = new Gender() { Type = "Male" },
            //    TripsNumber = 2334,
            //    LastName = "Esam",
            //};
            // await Usermanger.CreateAsync(user1, "Sara@ask123.com");

            //ApplicationUser user2 = new ApplicationUser()
            //{
            //    Email = "akg9397@gmail.com",
            //    UserName = "akg",
            //    NationalId = "12345678998752",
            //    FirstName = "Mohamed",
            //    Gender = new Gender() { Type = "Male" },
            //    TripsNumber = 2334,
            //    LastName = "Esam",
            //};
            //await Usermanger.CreateAsync(user2, "Sara@ask123.com");

            //var role = new IdentityRole("Admin");
            //await _roleManager.CreateAsync(role);

            //var SecondRole = new IdentityRole("user");
            //await _roleManager.CreateAsync(SecondRole);

            //await Usermanger.AddToRoleAsync(user1, "Admin");
            //await Usermanger.AddToRoleAsync(user2, "user");
            #endregion

            Email = "MohamedEsam";
            Password = "Sara@123.com";

            var result = await SignInManager.PasswordSignInAsync(Email, Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("./adminDashboard");
            }
            return RedirectToPage("./Login");
        }
    }
}