using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class VehicleBooking
    {
        public int Id { get; set; }
        public string HireDate { get; set; }
        public string ReturnData { get; set; }
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        public string RenterId { get; set; }
        [ForeignKey("RenterId")]
        public ApplicationUser Renter { get; set; }
    }
}
