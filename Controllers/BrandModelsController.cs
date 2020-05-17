using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LendCar.Models;
using LendCar.Repository;
using X.PagedList;

namespace LendCar.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandModelsController : Controller
    {
        private readonly IBrandModelRepository _bmRepo;

        public BrandModelsController(IBrandModelRepository bmRepo)
        {
            _bmRepo = bmRepo;
        }
        [ActionName("GetBrandModelsPartial")]
        [HttpGet("{brandId}")]
        public IActionResult BrandModelsList([FromRoute]int brandId = 0,int page = 1)
        {
            if(brandId != 0)
                return PartialView("_BrandModelsList", _bmRepo.GetAllBrandModels().Where(bm=>bm.BrandId == brandId).GroupBy(bm=>bm.Brand.Name).ToPagedList(page, 10));
            else
                return PartialView("_BrandModelsList", _bmRepo.GetAllBrandModels().GroupBy(bm=>bm.Brand.Name).ToPagedList(page, 10));

        }

        [ActionName("GetBrandModel")]
        [HttpGet("{id}")]
        public IActionResult GetBrandModel(int id)
        {
            var brandModel = _bmRepo.GetBrandModel(id);

            if (brandModel == null)
            {
                return NotFound();
            }

            return Ok(brandModel);
        }


        // PUT: api/Brands/edit/id/page
        [ActionName("Edit")]
        [HttpPost("{id}/{page}")]
        public IActionResult Edit([FromRoute]int id, [FromBody] BrandModel brandModel, [FromRoute]int page = 1)
        {
            IActionResult result;
            if (id != brandModel.Id)
            {
                result = BadRequest();
            }

            _bmRepo.Context.Entry(brandModel).State = EntityState.Modified;

            try
            {
                _bmRepo.Save();
            }
            catch (DbUpdateConcurrencyException err)
            {
                if (!BrandExists(id))
                {
                    result = NotFound();
                }
                else
                {
                    result = BadRequest(err.ToString());
                }
            }

            result = PartialView("_BrandModelsList", _bmRepo.GetAllBrandModels().ToList().ToPagedList(page, 10));

            return result;
        }


        [ActionName("Delete")]
        [HttpPost("{id}/{page}")]
        public IActionResult Delete([FromRoute]int id, [FromRoute] int page = 1)
        {
            var brandModel = _bmRepo.GetBrandModel(id);
            if (brandModel == null)
            {
                return NotFound();
            }

            _bmRepo.Delete(id);
            _bmRepo.Save();
            return PartialView("_BrandModelsList", _bmRepo.GetAllBrandModels().ToList().ToPagedList(page, 10));
        }

        [ActionName("Add")]
        [HttpPost("{page}")]
        public IActionResult Add([FromBody]BrandModel brandModel, [FromRoute] int page = 1)
        {
            _bmRepo.Add(brandModel);
            _bmRepo.Save();
            return PartialView("_BrandModelsList", _bmRepo.GetAllBrandModels().ToList().ToPagedList(page, 10));
        }

        private bool BrandExists(int id)
        {
            return _bmRepo.Exists(id);
        }

        [ActionName("byBrand")]
        [HttpGet("{id}")]
        public IActionResult GetModelByBrandId([FromRoute]int id)
        {
            var brandModel = _bmRepo.GetAllBrandModels().Where(bm => bm.BrandId == id).OrderBy(bm => bm.Name);

            if (brandModel == null)
            {
                return NotFound();
            }

            return Ok(brandModel);
        }

    }
}
