using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace LendCar.Pages.Controllers
{
 
    public class ValidationController : Controller
    {
        public ValidationController(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager )
        {
            UserRepository = userRepository;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public IUserRepository UserRepository { get; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public UserManager<ApplicationUser> UserManager { get; }

        [HttpPost]
        public IActionResult IsUserNameExists([Bind(Prefix ="Input.UserName")]string userName)
        {
            if (UserRepository.IsUserNameExists(userName))
                return Json(false);
            return Json(true);
        }

        [HttpPost]
        public IActionResult IsNationalIdExist([Bind(Prefix = "Input.NationalId")]string NationalId)
        {
            if (UserRepository.IsNationalIdExist(NationalId))
                return Json(false);
            return Json(true);
        }
        [HttpPost]
        public IActionResult IsOldPasswordMatch([Bind(Prefix = "Input.OldPassword")]string OldPassword)
        {
            if (SignInManager.CheckPasswordSignInAsync(UserRepository.FindById(UserManager.GetUserId(User)), OldPassword, false).Result.Succeeded) 
                return Json(true);
            return Json(false);
        }
        public IActionResult IsEmailExists([Bind(Prefix ="Input.Email")]string Email)
        {
            if (UserRepository.IsEmailExists(Email))
                return Json(false);
            return Json(true);
        }
    }
}