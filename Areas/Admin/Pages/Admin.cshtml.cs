using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LendCar.Pages
{
    //[Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private ICarRepository _carRepository;
        private IBrandRepository _brandRepo;
        private IBrandModelRepository _brandModelRepo;
        public IPagedList<Vehicle> Vehicles { get; set; }
        public IPagedList<Brand> Brands { get; set; }
        public IPagedList<IGrouping<string,BrandModel>> BrandModels { get; set; }
        public SelectList BrandsSelectList { get; set; }
        public int CurrentVehiclesPage { get; set; }
        public int CurrentBrandsPage { get; set; }
        public int CurrentBrandModelsPage { get; set; }
        public int PageSize { get; set; }
        public AdminModel(ICarRepository carRepository,
                          IBrandRepository brandRepository,
                          IBrandModelRepository brandModelRepository) 
        {
            this._carRepository = carRepository;
            this._brandRepo = brandRepository;
            this._brandModelRepo = brandModelRepository;
        }
        public void OnGet()
        {
            BrandsSelectList = new SelectList(_brandRepo.GetAllBrands().OrderBy(b=>b.Name),"Id","Name");
            Request.Query.TryGetValue("entries", out var entries);
            Request.Query.TryGetValue("vehiclePage", out var vehiclePage);
            Request.Query.TryGetValue("brandPage", out var brandPage);
            Request.Query.TryGetValue("brandModelPage", out var brandModelPage);
            int vehiclesPageNumber = 1;
            int brandsPageNumber = 1;
            int brandModelsPageNumber = 1;
            if (entries.Count > 0)
            {
                if (int.TryParse(entries[0], out var _pageSize))
                    PageSize = _pageSize;
            }
            else
                PageSize = 5;
            if (vehiclePage.Count > 0)
                if (int.TryParse(vehiclePage[0], out var pageNum))
                    vehiclesPageNumber = pageNum;

            if (brandPage.Count > 0)
                if (int.TryParse(brandPage[0], out var pageNum))
                    brandsPageNumber = pageNum;
       
            if (brandModelPage.Count > 0)
                if (int.TryParse(brandModelPage[0], out var pageNum))
                    brandModelsPageNumber = pageNum;
          
            CurrentVehiclesPage = vehiclesPageNumber;
            CurrentBrandsPage = brandsPageNumber;
            CurrentBrandModelsPage = brandModelsPageNumber;

            Vehicles = _carRepository.GetAllVehiclesRequests().ToList().ToPagedList(vehiclesPageNumber, PageSize);
            Brands = _brandRepo.GetAllBrands().ToList().ToPagedList(brandsPageNumber, PageSize);
            BrandModels = _brandModelRepo.GetAllBrandModels().GroupBy(bm=>bm.Brand.Name).ToPagedList(brandModelsPageNumber, PageSize);
        }
    }
}