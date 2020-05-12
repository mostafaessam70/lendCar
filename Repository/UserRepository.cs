using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
using LendCar.Models;
using Microsoft.EntityFrameworkCore;

namespace LendCar.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(LendCarDBContext Context)
        {
            this.Context = Context;
        }
        public LendCarDBContext Context { get; }
        public List<ApplicationUser> GetAllUsers() => Context.Users.Include(u => u.City)
                                                                    .Include(u => u.Gender)
                                                                    .ToList();
        public ApplicationUser FindByNatId(string id) => Context.Users.Include(u => u.City)
                                                                       .Include(u => u.Gender)
                                                                       .SingleOrDefault(p => p.NationalId == id);
        public ApplicationUser FindById(string id) => Context.Users.Include(u=>u.City)
                                                                   .Include(u=>u.Gender)
                                                                   .SingleOrDefault(p => p.Id == id);
        public void Save() => Context.SaveChanges();

    }
}
