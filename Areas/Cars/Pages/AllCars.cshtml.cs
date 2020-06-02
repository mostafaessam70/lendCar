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
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LendCar.Pages
{
    public class AllCarsModel : PageModel
    {
        private readonly ILogger<AllCarsModel> _logger;
        public ICarRepository ICarRepository { get; }

        public IPagedList<Vehicle> Vehicles { get; set; }
        public AllCarsModel(ILogger<AllCarsModel> logger, ICarRepository ICarRepository,SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            this.ICarRepository = ICarRepository;
        }

        public void OnGet()
        {
            Request.Query.TryGetValue("Page", out var page);
            int pageNumber;
            if (page.Count > 0)
            {
                if (int.TryParse(page[0], out var pageNum))
                {
                    pageNumber = pageNum;
                }
                else
                    pageNumber = 1;
            }
            else
                pageNumber = 1;

            Vehicles = ICarRepository.GetAllAvailableVechilces().ToPagedList(pageNumber, 9);   
        }
    }
}
