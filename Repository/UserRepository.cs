using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
using LendCar.Models;

namespace LendCar.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(LendCarDBContext Context)
        {
            this.Context = Context;
        }
        public LendCarDBContext Context { get; }
        public ApplicationUser FindByNatId(string id) => Context.Users.SingleOrDefault(p => p.NationalId == id);
        public ApplicationUser FindById(string id) => Context.Users.SingleOrDefault(p => p.Id == id);
        public void EditBookingInfo(ApplicationUser user)
        {
            var appUser = FindById(user.Id);
            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;
            appUser.PhoneNumber = user.PhoneNumber;
            appUser.DriverLicenseNumber = user.DriverLicenseNumber;
            appUser.CityId = user.CityId;
            appUser.BirthDate = user.BirthDate;
        }
        public void Save() => Context.SaveChanges();

    }
}
