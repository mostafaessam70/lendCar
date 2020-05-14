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
        private ICarRepository _carRepository;
        private IBrandRepository _brandRepo;
        public IPagedList<Vehicle> Vehicles { get; set; }
        public IPagedList<Brand> Brands { get; set; }
        public int CurrentVehiclesPage { get; set; }
        public int CurrentBrandsPage { get; set; }
        public AdminModel(ICarRepository carRepository,
                          IBrandRepository brandRepository) 
        {
            this._carRepository = carRepository;
            this._brandRepo = brandRepository;
        }
        public void OnGet()
        {
            Request.Query.TryGetValue("vehiclePage", out var vehiclePage);
            Request.Query.TryGetValue("brandPage", out var brandPage);
            int pageNumber;
            if (vehiclePage.Count > 0)
            {
                if (int.TryParse(vehiclePage[0], out var pageNum))
                {
                    pageNumber = pageNum;
                }
                else
                    pageNumber = 1;
            }
            else
                pageNumber = 1;

            if (brandPage.Count > 0)
            {
                if (int.TryParse(brandPage[0], out var pageNum))
                {
                    pageNumber = pageNum;
                }
                else
                    pageNumber = 1;
            }
            else
                pageNumber = 1;

            CurrentVehiclesPage = pageNumber;
            CurrentBrandsPage = pageNumber;

            Vehicles = _carRepository.GetAllVehiclesRequests().ToList().ToPagedList(pageNumber, 10);
            Brands = _brandRepo.GetAllBrands().ToList().ToPagedList(pageNumber, 10);

        }
    }
}