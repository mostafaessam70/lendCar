using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
namespace LendCar.Repository
{
    interface IPersonRepository
    {
        LendCarDBContext Context { get; }
        ApplicationUser Find(string id);
        void Save();
    }
}
