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
        public Vehicle GetVehicle(int id) => Context.Vehicles.Include(v => v.Color)
                                     .Include(c => c.Owner)
                                     .Include(v => v.Photos)
                                     .Include(v => v.Model)
                                     .ThenInclude(v => v.Brand)
                                     .SingleOrDefault(c => c.Id == id);
        public IQueryable<Vehicle> GetAllVehiclesAccepted() => Context.Vehicles
                                     .Include(v => v.Photos)
                                     .Include(v => v.Color)
                                     .Include(c => c.City)
                                     .Include(v => v.Model)
                                    .ThenInclude(v => v.Brand).Where(v => v.AcceptedAdmin == true);
        public IQueryable<Vehicle> GetAllVehiclesRequests() => Context.Vehicles
                                     .Include(v => v.Photos)
                                     .Include(v => v.Color)
                                     .Include(c => c.City)
                                     .Include(v => v.Model)
                                     .ThenInclude(v => v.Brand).Where(v => v.AcceptedAdmin == false);

        public bool IsCarAvailable(string startDate, string endDate, int carId)
        {
            var sDate = Convert.ToDateTime(startDate);
            var eDate = Convert.ToDateTime(endDate);
            Vehicle vehicle = GetVehicle(carId);


            if (sDate.CompareTo(eDate) < 0)
            {

                var returnDate = Context.VehicleBookings.Where(c => c.VehicleId == carId)
                 ?.Select(c => Convert.ToDateTime(c.ReturnData)).Max();

                return (sDate.CompareTo(returnDate) > 0 &&
                    endDate?.CompareTo(vehicle.EndDate) <= 0 && sDate.CompareTo(vehicle.StartDate) >= 0);
            }
            return false;
        }
        public string GetBookingEndDate(int carId)
        {
            var bookingList = Context.VehicleBookings.Where(c => c.VehicleId == carId);
            if (bookingList.Count() > 0)
            {
              return  bookingList.Select(c => Convert.ToDateTime(c.ReturnData)).Max().ToShortDateString();
            }
            return GetVehicle(carId).EndDate;
        }
          
        public string GetBookingStartDate(int carId) =>
              Context.VehicleBookings.Where(c => c.VehicleId == carId)
                 ?.Select(c => Convert.ToDateTime(c.HireDate)).Max().ToShortDateString();

        public void Add(Vehicle vehicle) => Context.Vehicles.Add(vehicle);
        public void Delete(int id) => Context.Vehicles.Remove(GetVehicle(id));

        public void Save() => Context.SaveChanges();

    }
}
