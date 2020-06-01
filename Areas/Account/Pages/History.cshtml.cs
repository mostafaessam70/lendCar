using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Areas.Account.Pages
{
    public class HistoryModel : PageModel
    {
        public List<VehicleBooking> UserBookingCars;
        public List<Vehicle> UserCars;
        private readonly UserManager<ApplicationUser> UserManager;

        public ICarRepository ICarRepository { get; }
        public IClientRepository ClientRepository { get; }

        public HistoryModel(ICarRepository ICarRepository, UserManager<ApplicationUser> userManager,
            IClientRepository clientRepository)
        {
            this.ICarRepository = ICarRepository;
            UserManager = userManager;
            ClientRepository = clientRepository;
        }


        public void OnGet()
        {
            var userId = UserManager.GetUserId(User);
            UserBookingCars = ICarRepository.GetAllBookingByUserID(userId);
            UserCars = ICarRepository.GetAllUserCar(userId);
        }
        public void OnPost(int bookingId)
        {
            var booking = ICarRepository.GetAllBooking().SingleOrDefault(c => c.Id == bookingId);
            if (Convert.ToDateTime(booking.HireDate).Date > DateTime.Now.Date)
            {
                ClientRepository.CancelBooking(bookingId);
                ClientRepository.Save();
            }
        }
    }
}
