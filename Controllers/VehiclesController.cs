using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LendCar.Controllers
{
    [Route("Vehicles")]
    public class VehiclesController : Controller
    {
        public ICarRepository CarRepository { get; }

        public VehiclesController(ICarRepository _CarRepository)=>
            CarRepository = _CarRepository;

        [Route("VehiclesList")]
        public PartialViewResult VehiclesList(int page =1)=>
             PartialView("_CarList",CarRepository.GetAllVehicles().ToList().ToPagedList(page, 9));

        [Route("Delete")]
        public PartialViewResult Delete(int id , int page=1)
        {
            CarRepository.Delete(id);
            CarRepository.Save();
            return PartialView("_AdminCarList", CarRepository.GetAllVehicles().ToList().ToPagedList(page, 10));
        }

    }
}