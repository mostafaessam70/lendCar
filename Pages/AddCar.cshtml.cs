using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Pages
{
    public class AddCarModel : PageModel
    {
        private IVehicleTypeRepository vehicleTypeRepo;
        private IBrandRepository brandRepo;
        private IBrandModelRepository brandModelRepo;
        private ICarRepository carRepo;
        private IWebHostEnvironment hostEnvironment;

        public AddCarModel(IVehicleTypeRepository _vehicleTypeRepo,
                            ICarRepository _carRepo,
                            IBrandRepository _brandRepo,
                            IBrandModelRepository _brandModelRepo,
                            IWebHostEnvironment _hostEnvironment)
        {
            this.vehicleTypeRepo = _vehicleTypeRepo;
            this.carRepo = _carRepo;
            this.brandRepo = _brandRepo;
            this.brandModelRepo = _brandModelRepo;
            this.hostEnvironment = _hostEnvironment;
            this.VehicleTypes = new SelectList(vehicleTypeRepo.GetAllVehicleTypes().OrderBy(vt => vt.Type), "Id", "Type");
            this.Brands = new SelectList(brandRepo.GetAllBrands().OrderBy(b => b.Name), "Id", "Name");
            this.BrandModels = new SelectList(brandModelRepo.GetAllBrandModels().Where(bm => bm.BrandId == brandRepo.GetAllBrands().OrderBy(b => b.Name).FirstOrDefault().Id).OrderBy(b => b.Name), "Id", "Name");
            this.OdoMeters = new SelectList(carRepo.Context.OdoMeters.ToList(), "Id", "Value");
            this.Colors = carRepo.Context.Colors.OrderBy(c => c.Name).ToList();
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; }
        [BindProperty,Required(ErrorMessage ="Car photos must be included")]
        public IEnumerable<IFormFile> VehiclePhotos { get; set; }
        public SelectList Brands { get; set; }
        public SelectList BrandModels { get; set; }
        public SelectList VehicleTypes { get; set; }
        public SelectList OdoMeters { get; set; }
        public List<Color> Colors { get; set; }

        public void OnGet() 
        {
            
        }
        public IActionResult OnPost(Vehicle Vehicle,IEnumerable<IFormFile> VehiclePhotos) 
        {

            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }
            //else if () 
            //{

            //    return RedirectToPage();

            //}
            else
            {
                if (VehiclePhotos != null && VehiclePhotos.Count() > 0)
                {
                    string newImgName = null;
                    List<CarImage> photos = new List<CarImage>();
                    foreach (var photo in VehiclePhotos)
                    {
                        string folder = Path.Combine(hostEnvironment.WebRootPath, "CarPhotosUploaded");
                        newImgName = $"{DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss")}_{photo.FileName}";
                        string file = Path.Combine(folder, newImgName);
                        FileStream fs = new FileStream(file, FileMode.Create);
                        photo.CopyTo(fs);
                        fs.Close();
                        photos.Add(new CarImage { Image = newImgName });
                        if (VehiclePhotos.ElementAt(0) == photo)
                        {
                            Vehicle.ImageUrl = newImgName;
                        }
                    }
                    Vehicle.Photos = photos;

                    carRepo.Add(Vehicle);
                    carRepo.Save();
                    return RedirectToPage("/Index");

                }
                else
                    return RedirectToPage();
            }
        }
    }
}
