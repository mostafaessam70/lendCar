using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace LendCar.Pages
{
    //[Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        public ICarRepository ICarRepository { get; }
        public IPagedList<Vehicle> Vehicles { get; set; }
        public int CurrentPage { get; set; }
        public AdminModel(ICarRepository ICarRepository)
        {
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

            CurrentPage = pageNumber;

            Vehicles = ICarRepository.GetAllVehiclesRequests().ToList().ToPagedList(pageNumber, 10);
        }
    }
}