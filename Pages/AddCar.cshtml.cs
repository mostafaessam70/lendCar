using LendCar.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Pages
{
    public class AddCarModel:PageModel
    {
        private ICarRepository carRepository;
        public AddCarModel(ICarRepository _carRepository)
        {
            this.carRepository = _carRepository;
        }
    }
}
