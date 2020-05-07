using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using LendCar.Models;
using LendCar.Repository;

namespace LendCar.Pages
{
    public class IndexModel : PageModel
    {
     

        private readonly ILogger<IndexModel> _logger;
        public List<Vehicle> Vehicles { get; set; }
        public ICarRepository ICarRepository { get; }

        public IndexModel(ILogger<IndexModel> logger,ICarRepository ICarRepository)
        {
            _logger = logger;
            this.ICarRepository = ICarRepository;
        }

        public void OnGet()
        {
            Vehicles = ICarRepository.GetAllVehicles();
        }
     
    }
}
