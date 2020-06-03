using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LendCar.Pages.Controllers
{
 
    public class ValidationController : Controller
    {
        public ValidationController(IUserRepository userRepository )
        {
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository { get; }

        [HttpPost]        
        public IActionResult IsNationalIdExist([Bind(Prefix = "Input.NationalId")]string NationalId)
        {
            if (UserRepository.IsNationalIdExist(NationalId))
                return Json(false);
            return Json(true);
        }
    }
}