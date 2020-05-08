using LendCar.DBContext;
using LendCar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class CarRepository : ICarRepository
    {
        public CarRepository(LendCarDBContext Context)
        {
            this.Context = Context;
        }
        public LendCarDBContext Context { get; }
        public Vehicle GetVehicle(int id) => Context.Vehicles.SingleOrDefault(c => c.Id == id);
        public IQueryable<Vehicle> GetAllVehicles() => Context.Vehicles;
        public void Add(Vehicle vehicle) => Context.Vehicles.Add(vehicle);
        public void Delete(int id) => Context.Vehicles.Remove(GetVehicle(id));
        public void Save() => Context.SaveChanges();
    }
}
