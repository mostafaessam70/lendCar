using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        public UsersModel(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole>  RoleManager { get; }

        public void OnGet()
        {
            
        }
    }
}
