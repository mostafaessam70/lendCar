using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace LendCar.Pages
{
    public class HomeModel : PageModel
    {
        private SignInManager<ApplicationUser> _signInManager;
        private ICarRepository _carRepo;
        public List<Vehicle> Vehicles { get; set; }
        public HomeModel(SignInManager<ApplicationUser> signInManager, ICarRepository carRepo)
        {
            _signInManager = signInManager;
            _carRepo = carRepo;
        }
        public void OnGet()
        {
            Vehicles = _carRepo.GetAllVehiclesAccepted().OrderBy(v => v.Rate).Take(3).ToList();
        }

        public IActionResult OnGetLogout()
        {
            this._signInManager.SignOutAsync();
            return RedirectToPage("Index");
        }

    }
}