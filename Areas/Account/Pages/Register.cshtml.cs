using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LendCar.Models;
using Microsoft.Extensions.Logging;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using LendCar.DBContext;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;


namespace LendCar.Pages
{
    [AllowAnonymous]

    public class RegisterModel : PageModel
    {
  
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IWebHostEnvironment _hostEnvironment;

        public SelectList Cities { get; set; }
        public SelectList Genders { get; set; }
        public LendCarDBContext _context { get; }
      
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            LendCarDBContext Context,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hostEnvironment
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = Context;
            _roleManager = roleManager;
            _hostEnvironment = hostEnvironment;
            Cities = new SelectList(_context.Cities.OrderBy(c => c.Name).ToList(), "Id", "Name");
            Genders = new SelectList(_context.Genders.ToList(), "Id", "Type");
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [Display(Name = "UserName")]
            [Remote("IsUserNameExists", "Validation", ErrorMessage ="User Name already Exist",HttpMethod ="POST")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [Remote("IsEmailExists", "Validation", ErrorMessage = "Email already Exist", HttpMethod = "POST")]
            public string Email { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            //[RegularExpression ("?=.*[A - Z])",ErrorMessage = "Password must contain at least One Uppercase letter ")]
            public string Password { get; set; }
            //mohamedESAM9397..
           
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Remote("IsNationalIdExist", "Validation",
                ErrorMessage = "Nationl Id Already Exist",HttpMethod ="POST")]
            [Required]
            [MinLength(14,ErrorMessage = "the National Id be 14 numbers")]
            [RegularExpression("[0-9]{14}", ErrorMessage = "Invalid National ID ")]
            public string NationalId { get; set; }
            [Required]
            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "DriverLicenseNumber")]
            public string DriverLicenseNumber { get; set; }


            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }

            public IFormFile ProfilePicture { get; set; }
            public int CityId { get; set; }
            public int GenderId { get; set; }
        }

        public async Task OnGetAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
             if (ModelState.IsValid)
            {
                string newImgName = "default.jpg";
                if (Input.ProfilePicture != null && Input.ProfilePicture.Length > 0)
                {
                    string folder = Path.Combine(_hostEnvironment.WebRootPath, "images/users");
                    newImgName = $"{Input.UserName}_{DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss")}{Path.GetExtension(Input.ProfilePicture.FileName)}";
                    string file = Path.Combine(folder, newImgName);
                    FileStream fs = new FileStream(file, FileMode.Create);
                    Input.ProfilePicture.CopyTo(fs);
                    fs.Close();
                }

                var user = new ApplicationUser { UserName = Input.UserName,
                    Email = Input.Email,
                    JoinedAt = DateTime.Now.ToString("dd-MM-yyyy"),
                    DriverLicenseNumber = Input.DriverLicenseNumber,
                    PhoneNumber = Input.PhoneNumber,CityId = Input.CityId,
                    Address = Input.Address, GenderId = Input.GenderId,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    ImageUrl = $"~/images/users/{newImgName}",
                    NationalId = Input.NationalId
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                  
                    if (!await _roleManager.RoleExistsAsync("user"))
                    {
                        await _roleManager.CreateAsync(new  IdentityRole("user") );

                    }

                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, false, false);
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}