using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Pages
{
    public class carDetailsModel : PageModel
    {
        public Vehicle CurrentCar { get; set; }
        public ApplicationUser Owner { get; set; }

        public List<CarImage> CurrentCarImges { get; set; }
        public ICarRepository CarRepository { get; }
        public IUserRepository UserRepository { get; }

        [BindProperty]
        public string EndBookingDate { get; set; }
        [BindProperty]
        public string StartBookingDate { get; set; }
        public carDetailsModel(ICarRepository _CarRepository,IUserRepository _UserRepository)
        {   
            CarRepository = _CarRepository;
            UserRepository = _UserRepository;
        }
        public  IActionResult OnGet(int id)
        {
            CurrentCar = CarRepository.GetVehicle(id);
            StartBookingDate = Convert.ToDateTime(CurrentCar.StartDate).ToString("yyyy-MM-dd");
            EndBookingDate = Convert.ToDateTime(CurrentCar.EndDate).ToString("yyyy-MM-dd");
            CurrentCarImges = CurrentCar?.Photos.ToList();
            Owner = UserRepository.FindById(CurrentCar?.OwnerId);
            return  Page();
        }
        public IActionResult OnPost(string endBookingDate,string startBookingDate,int carId)
        {
            if (ModelState.IsValid)
            {
                //if (CarRepository.IsCarAvailable(startBookingDate, endBookingDate, carId))
                {
                    return RedirectToPage("booking",
                        new
                        {
                            area = "Cars",
                            EndBookingDate = endBookingDate,
                            StartBookingDate = startBookingDate,
                            CarId = carId
                        });
                }
                //ViewData["errorMessage"] = "this Vehicle Avilabel only From {} to {}";
            }

           return OnGet(carId);
        }
    }
}