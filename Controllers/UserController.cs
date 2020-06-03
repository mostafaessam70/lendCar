using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LendCar.Pages.Controllers
{
    [Route("Users")]
    public class UserController : Controller
    {
        public UserController(UserManager<ApplicationUser> userManager,
            IUserRepository userRepository
            , RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            RoleManager = roleManager;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public IUserRepository UserRepository { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        [Route("ChangeRole")]
        public async Task<IActionResult> ChangeRole(string userId, string roleName)
        {

            var user = UserManager.Users.SingleOrDefault(c => c.Id == userId);

            await UserManager.RemoveFromRolesAsync(user, RoleManager.Roles.Select(c => c.Name).ToList());

            var result = await UserManager
                 .AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Json("Succeed");
            }

            return Json("Failed");
        }
    }
}