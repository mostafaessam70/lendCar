using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LendCar.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult ErrorHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:return RedirectToPage("/NotFound");
                default:return RedirectToPage("/Error");
            }
        }
    }
}