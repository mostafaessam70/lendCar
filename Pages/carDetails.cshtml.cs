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

        public List<Img> CurrentCarImges;
        public ICarRepository CarRepository { get; }
        public IUserRepository UserRepository { get; }

        public carDetailsModel(ICarRepository _CarRepository,IUserRepository _UserRepository)
        {
            CarRepository = _CarRepository;
            UserRepository = _UserRepository;
        }
        public void OnGet(string id)
        {
            CurrentCar = CarRepository.Find("1FTRW14W84KC76110");
            CurrentCarImges = CarRepository.GetImgForCurrntCar("1FTRW14W84KC76110");
            Owner =UserRepository.Find(CurrentCar.OwnerId);
        }
    }
}