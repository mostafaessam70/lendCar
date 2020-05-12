using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
namespace LendCar.Repository
{
    public interface IUserRepository
    {
        LendCarDBContext Context { get; }
        ApplicationUser FindByNatId(string id);
        ApplicationUser FindById(string id);
        void EditBookingInfo(ApplicationUser user);
        void Save();
    }
}
