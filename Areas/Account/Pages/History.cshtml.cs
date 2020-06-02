using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using LendCar.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Areas.Account.Pages
{
    public class HistoryModel : PageModel
    {
        public ClientHistoryViewModel ClientHistory;

        private readonly UserManager<ApplicationUser> UserManager;

        public ICarRepository ICarRepository { get; }
        public IClientRepository ClientRepository { get; }
        public string UserId { get;  set; }

        public HistoryModel(ICarRepository ICarRepository, UserManager<ApplicationUser> userManager,
            IClientRepository clientRepository)
        {
            this.ICarRepository = ICarRepository;
            UserManager = userManager;
            ClientRepository = clientRepository;
            ClientHistory = new ClientHistoryViewModel();
        }


        public void OnGet()
        {
            UserId = UserManager.GetUserId(User);
            ClientHistory.UserBookingCars = ICarRepository.GetAllBookingByUserID(UserId)
                 .Where(c => !c.IsBookingCanceled).ToList(); 
            ClientHistory.UserCars = ICarRepository.GetAllUserCar(UserId);
        }
        public void OnPost(int bookingId)
        {

        }
    }
}
