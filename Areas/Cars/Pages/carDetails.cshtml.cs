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

        public carDetailsModel(ICarRepository _CarRepository,IUserRepository _UserRepository)
        {
            CarRepository = _CarRepository;
            UserRepository = _UserRepository;
        }
        public void OnGet(int id)
        {
            CurrentCar = CarRepository.GetVehicle(1);
            CurrentCarImges = CurrentCar.Photos.ToList();
            Owner = UserRepository.FindById(CurrentCar.OwnerId);
        }
    }
}