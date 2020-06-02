using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using LendCar.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LendCar.Controllers
{
    [Route("Client")]
    public class ClientController : Controller
    {
        private readonly ICarRepository carRepository;

        public SignInManager<ApplicationUser> SignInManager { get; }

        public ClientController(ICarRepository carRepository, IClientRepository clientRepository, IUserRepository userRepository, SignInManager<ApplicationUser> signInManager)
        {
            this.carRepository = carRepository;
            ClientRepository = clientRepository;
            UserRepository = userRepository;
            SignInManager = signInManager;
        }

        public IClientRepository ClientRepository { get; }
        public IUserRepository UserRepository { get; }

        [Route("Deliver")]
        public PartialViewResult DeliverMoneyToClient(string clientId)
        {
            ClientRepository.DeliveringMoneyToVechicleOnwer(clientId);
            ClientRepository.Save();
            return PartialView("_ClientMoney", ClientRepository.GetUserMoney());
        }
        [Route("Cancel")]
        public PartialViewResult ClientCancelMoney(string clientId)
        {
            ClientRepository.CancelMoneyBack(clientId);
            ClientRepository.Save();
            return PartialView("_ClientCancel", ClientRepository.GetClientsCanceledBooking());
        }
        [Route("ClientCancel")]
        public PartialViewResult bookingCanceledByClient(int bookingId, string userID)
        {
            var booking = carRepository.GetAllBooking().SingleOrDefault(c => c.Id == bookingId);
            if (Convert.ToDateTime(booking.HireDate).Date > DateTime.Now.Date)
            {
                ClientRepository.CancelBooking(bookingId);
                ClientRepository.Save();
            }
            var UserBookingCars = carRepository.GetAllBookingByUserID(userID)
                .Where(c=>!c.IsBookingCanceled).ToList();

            var UserCars = carRepository.GetAllUserCar(userID);

            ClientHistoryViewModel clientHistory = new ClientHistoryViewModel(){
                UserBookingCars = UserBookingCars,
                UserCars = UserCars
            };
            return PartialView("_History", clientHistory);
        }

        [Route("EditInfo")]
        public IActionResult EditClientInfo(ApplicationUser client)
        {
            if (ModelState.IsValid)
            {
                UserRepository.EditBookingInfo(client);
                UserRepository.Save();
                return Ok("ok");
            }
            return Ok("failed");
        }


    }
}