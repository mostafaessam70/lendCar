using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LendCar.Areas.Cars
{
    public class carEditModel : PageModel
    {
        [BindProperty]
        public Vehicle Vehicle { get; set; }
        public SelectList Brands { get; set; }
        public SelectList BrandModels { get; set; }
        public SelectList VehicleTypes { get; set; }
        public SelectList OdoMeters { get; set; }
        public SelectList Colors { get; set; }
        public SelectList Cities { get; set; }
        public IEnumerable<IFormFile> VehiclePhotos { get; set; }
        public DateTime Today { get; set; }
        private IWebHostEnvironment hostEnvironment;
        public UserManager<ApplicationUser> userManager { get; set; }


        public ICarRepository carRepo { get; }
        private IVehicleTypeRepository vehicleTypeRepo;
        private IBrandRepository brandRepo;
        private IBrandModelRepository brandModelRepo;
        private List<CarImage> carImages;
        public LendCarDBContext _context { get; }
        public carEditModel(IVehicleTypeRepository _vehicleTypeRepo,
                           ICarRepository _carRepo,
                           IBrandRepository _brandRepo,
                            LendCarDBContext Context,
                           IBrandModelRepository _brandModelRepo, 
                           IWebHostEnvironment _hostEnvironment,
                           UserManager<ApplicationUser> _userManager
                           )
        {
            vehicleTypeRepo = _vehicleTypeRepo;
            carRepo = _carRepo;
            brandRepo = _brandRepo;
            brandModelRepo = _brandModelRepo;
            _context = Context;
            hostEnvironment = _hostEnvironment;
            userManager = _userManager;

            VehicleTypes = new SelectList(vehicleTypeRepo.GetAllVehicleTypes().OrderBy(vt => vt.Type), "Id", "Type");
            Brands = new SelectList(brandRepo.GetAllBrands().OrderBy(b => b.Name), "Id", "Name");
            OdoMeters = new SelectList(carRepo.Context.OdoMeters.ToList(), "Id", "Value");
            Colors = new SelectList(carRepo.Context.Colors.OrderBy(c => c.Name).ToList(), "Id", "Name");
            Vehicle = new Vehicle();
            Cities = new SelectList(carRepo.Context.Cities.OrderBy(c => c.Name).ToList(), "Id", "Name");
            Today = DateTime.Now.Date;
        }


        public void OnGet(int id)
        {
            Vehicle = carRepo.GetVehicle(id);
            BrandModels = new SelectList(brandModelRepo.GetAllBrandModels().Where(bm => bm.BrandId == Vehicle.Model.BrandId).OrderBy(b => b.Name), "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Vehicle).State = EntityState.Modified;
            carImages = _context.CarImages.Where(i => i.VehicleId == Vehicle.Id).ToList();
            if (VehiclePhotos != null && VehiclePhotos.Count() >= 4)
            {
                foreach (var photo in carImages)
                {
                    _context.CarImages.Remove(photo);
                    System.IO.File.Delete(Path.Combine(hostEnvironment.WebRootPath, photo.Image.Replace("~/", "")));
                }
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
                    photos.Add(new CarImage { Image = $"~/CarPhotosUploaded/{newImgName}" });
                    if (VehiclePhotos.ElementAt(0) == photo)
                    {
                        Vehicle.ImageUrl = $"~/CarPhotosUploaded/{newImgName}";
                    }
                }
                Vehicle.Photos = photos;

                await _context.SaveChangesAsync();

                return RedirectToPage("Profile", new { area="User" });
            }
            else if(VehiclePhotos != null && VehiclePhotos.Count() < 4 && VehiclePhotos.Count() > 0)
            {
                return RedirectToPage("EditCar",new { area = "Cars", id = Vehicle.Id });
            }
            else
            
                await _context.SaveChangesAsync();
            
            return RedirectToPage("Profile", new { area="User"});

        }
    }
}

