using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    interface ICarRepository
    {
        LendCarDBContext Context { get; }
        Vehicle Find(string id);
        void Save();
    }
}
