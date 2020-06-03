using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;
using X.PagedList.Mvc;
namespace LendCar.Pages
{
    public class userProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepo;
        private string _uid;
        private ICarRepository _carRepo;
        public ApplicationUser CurrentUser { get; set; }
        public IPagedList<Vehicle> UserVehicles { get; set; }
        public int PageSize { get; set; } = 3;

        public userProfileModel(UserManager<ApplicationUser> userManager,
                                IUserRepository userRepo,
                                ICarRepository carRepo)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _carRepo = carRepo;
        }
        public IActionResult OnGet()
        {
            Request.Query.TryGetValue("CarsPage", out var CarsPage);
            int PageNumber = 1;
            if (CarsPage.Count > 0)
                if (int.TryParse(CarsPage[0], out var pageNum))
                    PageNumber = pageNum;
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Login",new { area = "Account"});
            }
            _uid = _userManager.GetUserId(User);
            CurrentUser = _userRepo.FindById(_uid);
            UserVehicles = _carRepo.GetAllVehiclesAccepted().Where(v => v.OwnerId == _uid).ToList().ToPagedList(PageNumber, PageSize);
            return Page();
        }
    }
}