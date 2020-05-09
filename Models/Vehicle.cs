using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required,RegularExpression("[A-Z|0-9]{17}",ErrorMessage ="VIN is invalid")]
        public string VIN { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PlateNumber { get; set; }
        public string Year { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfDoors { get; set; }
        public double GasMileage { get; set; }
        public string EnergyMakeCarMove { get; set; }
        public int? TripsNumber { get; set; }
        public double PricePerDay { get; set; }
        public string StartDate { get; set; } 
        public string EndDate { get; set; } 
        public string Color { get; set; }
        [Range(1, 5)]
        public double? Rate { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Img> Photos { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }

        public int VehicleTypeId { get; set; }
        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }

        public int OdoMeterId { get; set; }
        [ForeignKey("OdoMeterId")]
        public OdoMeter OdoMeter { get; set; }

        public int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public BrandModel Model { get; set; }
    }
}
