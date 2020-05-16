using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Areas.Cars.Pages
{
    public class UserProfileModel : PageModel
    {
        public IUserRepository UserRerpository { get; }
        public ApplicationUser LoggedUserInfo { get; set; }
        private ICarRepository _carRepo;
        public List<Vehicle> UserVehicles { get; set; }
        public UserProfileModel(IUserRepository UserRerpository)
        {
            this.UserRerpository = UserRerpository;
        }

        public void OnGet()
        {
            LoggedUserInfo = UserRerpository.FindById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            UserVehicles = _carRepo.GetAllVehiclesAccepted().Where(v => v.OwnerId == LoggedUserInfo.Id).ToList();
        }
    }
}
