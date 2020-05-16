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
using Microsoft.AspNetCore.Cors;

namespace LendCar.Controllers
{
    [Route("api/[controller]/[action]")]

    public class BrandsController : Controller
    {
        private readonly IBrandRepository _brandRepo;

        public BrandsController(IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
        }

        [HttpGet]
        public IActionResult BrandsList(int page = 1) {
            return PartialView("_BrandsList", _brandRepo.GetAllBrands().ToList().ToPagedList(page, 10)); }
        [ActionName("GetBrand")]
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


        //[Route("{id}/{page}")]
        [ActionName("Delete")]
        [HttpPost("{id}/{page}")]
        public IActionResult Delete([FromRoute]int id,[FromRoute] int page = 1)
        {
            var brand = _brandRepo.GetBrand(id);
            if (brand == null)
            {
                return NotFound();
            }

            _brandRepo.Delete(id);
            _brandRepo.Save();
            return PartialView("_BrandsList", _brandRepo.GetAllBrands().ToList().ToPagedList(page, 10));
        }

        [ActionName("Add")]
        [HttpPost("{page}")]
        public IActionResult Add([FromBody]Brand brand,[FromRoute] int page = 1)
        {
            _brandRepo.Add(brand);
            _brandRepo.Save();
            return PartialView("_BrandsList", _brandRepo.GetAllBrands().ToList().ToPagedList(page, 10));
        }

        private bool BrandExists(int id)
        {
            return _brandRepo.Exists(id);
        }
    }
}
