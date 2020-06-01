using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LendCar.Controllers
{
    [Route("Client")]
    public class ClientController : Controller
    {
        public ClientController(IClientRepository clientRepository,IUserRepository userRepository)
        {
            ClientRepository = clientRepository;
            UserRepository = userRepository;
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

       /* public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
           
            return RedirectToPage("/Index");
            */

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