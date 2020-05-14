using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace LendCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        private readonly IBrandRepository _brandRepo;

        public BrandsController(IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
        }

        // GET: api/Brands
        [HttpGet]
        public PartialViewResult VehiclesList(int page = 1) { 
           return PartialView("_BrandsList", _brandRepo.GetAllBrands().ToList().ToPagedList(page, 10));}


        // GET: api/Brands/5
        [HttpGet("{id}")]
        public IActionResult GetBrand(int id)
        {
            var brand = _brandRepo.GetBrand(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }


        // PUT: api/Brands/5
        [HttpPut("{id}")]
        public IActionResult PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _brandRepo.Context.Entry(brand).State = EntityState.Modified;

            try
            {
                _brandRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Brands
        [HttpPost]
        public IActionResult PostBrand(Brand brand,int page = 1)
        {
            _brandRepo.Add(brand);
            _brandRepo.Save();
            return PartialView("_BrandsList", _brandRepo.GetAllBrands().ToList().ToPagedList(page, 10));
        }

    // DELETE: api/Brands/5
    [HttpPost]
        public IActionResult DeleteBrand(int id)
        {
            var brand = _brandRepo.GetBrand(id);
            if (brand == null)
            {
                return NotFound();
            }

            _brandRepo.Delete(id);
            _brandRepo.Save();
            return Ok(brand);
        }

        private bool BrandExists(int id)
        {
            return _brandRepo.Exists(id);
        }
    }
}
