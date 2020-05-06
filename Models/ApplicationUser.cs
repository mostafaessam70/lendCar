using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        [Required]
        public string NationalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        public string DriverLicenseNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string BriefAboutMe { get; set; }
        public Gender Gender { get; set; }
        [InverseProperty("Owner")]
        public virtual ICollection<Vehicle> VehiclesOwnedByHim { get; set; }

        [InverseProperty("Renter")]
        public virtual ICollection<Vehicle> VehiclesRentalByHim { get; set; }

        // public ICollection<Vehicle> Vehicles { get; set; }

    }


}
