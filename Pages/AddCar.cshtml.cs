using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Pages
{
    public class AddCarModel : PageModel
    {
        private IVehicleTypeRepository vehicleTypeRepo;
        private IBrandRepository brandRepo;
        private IBrandModelRepository brandModelRepo;
        private ICarRepository CarRepo;
        public SelectList VehicleTypes { get; set; }
        public int VehicleTypeId { get; set; }
        public SelectList Brands { get; set; }
        public int BrandId { get; set; }
        public SelectList BrandModels { get; set; }
        public int BrandModelId { get; set; }
        public AddCarModel(IVehicleTypeRepository vehicleTypeRepo,
                            ICarRepository carRepo,
                            IBrandRepository brandRepo,
                            IBrandModelRepository brandModelRepo)
        {
            this.vehicleTypeRepo = vehicleTypeRepo;
            this.CarRepo = carRepo;
            this.brandRepo = brandRepo;
            this.brandModelRepo = brandModelRepo;
        }
        public void OnGet() 
        {
            this.VehicleTypes = new SelectList(vehicleTypeRepo.GetAllVehicleTypes().OrderBy(vt=>vt.Type), "Id", "Type");
            this.Brands = new SelectList(brandRepo.GetAllBrands().OrderBy(b=>b.Name), "Id", "Name");
            this.BrandModels = new SelectList(brandModelRepo.GetAllBrandModels().OrderBy(b=>b.Name), "Id", "Name");
        }

    }
}
