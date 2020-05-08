using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LendCar.DBContext;
using LendCar.Models;
using LendCar.Repository;

namespace LendCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandModelsController : ControllerBase
    {
        private readonly IBrandModelRepository bmRepo;

        public BrandModelsController(IBrandModelRepository _bmRepo)
        {
            bmRepo = _bmRepo;
        }

        // GET: api/BrandModels
        [HttpGet]
        public IActionResult GetBrandModels()
        {
            return Ok(bmRepo.GetAllBrandModels());
        }

        // GET: api/BrandModels/5
        [HttpGet("{id}")]
        public IActionResult GetBrandModel(int id)
        {
            var brandModel = bmRepo.GetBrandModel(id);

            if (brandModel == null)
            {
                return NotFound();
            }

            return Ok(brandModel);
        }

        
        [HttpGet("byBrand/{id}")]
        public IActionResult GetModelByBrandId(int id)
        {
            var brandModel = bmRepo.GetAllBrandModels().Where(bm => bm.BrandId == id);

            if (brandModel == null)
            {
                return NotFound();
            }

            return Ok(brandModel);
        }

        // PUT: api/BrandModels/5
        [HttpPut("{id}")]
        public IActionResult PutBrandModel(int id, BrandModel brandModel)
        {
            if (id != brandModel.Id)
            {
                return BadRequest();
            }

            bmRepo.Context.Entry(brandModel).State = EntityState.Modified;

            try
            {
                bmRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandModelExists(id))
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

        // POST: api/BrandModels
        
        [HttpPost]
        public IActionResult PostBrandModel(BrandModel brandModel)
        {
            bmRepo.Add(brandModel);
            bmRepo.Save();

            return CreatedAtAction("GetBrandModel", new { id = brandModel.Id }, brandModel);
        }

        // DELETE: api/BrandModels/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBrandModel(int id)
        {
            var brandModel = bmRepo.GetBrandModel(id);
            if (brandModel == null)
            {
                return NotFound();
            }

            bmRepo.Delete(id);
            bmRepo.Save();
            return Ok(brandModel);
        }

        private bool BrandModelExists(int id)
        {
            return bmRepo.Exists(id);
        }
    }
}
