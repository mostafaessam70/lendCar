using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.ViewModels
{
    public class ClientHistoryViewModel
    {
       public List<VehicleBooking> UserBookingCars { get; set; }
       public List<Vehicle> UserCars { get; set; }
    }
}
