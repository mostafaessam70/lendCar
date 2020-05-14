using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public interface ICarRepository
    {
        LendCarDBContext Context { get; }
        Vehicle GetVehicle(int id);
        IQueryable<Vehicle> GetAllVehiclesAccepted();
        IQueryable<Vehicle> GetAllVehiclesRequests();
        void Add(Vehicle vehicle);
        void Delete(int id);
        void Save();
        bool IsCarAvailable(string startDate, string endDate, int carId);
    }
}
