using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace LendCar.Controllers
{
    [Route("Payment")]
    public class PaymentController : Controller
    {
        public PaymentController(ICarRepository ICarRepository)
        {
            this.ICarRepository = ICarRepository;
        }

        public ICarRepository ICarRepository { get; }

        [Route("Pay")]
        public IActionResult Pay(VehicleBooking bookingInfo, decimal amount, string stripeEmail, string stripeToken)
        {

            var c = new CustomerService();
            var customer = c.Create(new CustomerCreateOptions()
            {
                Email = stripeEmail,
                Source = stripeToken
            });
            var ch = new ChargeService();
            var change = ch.Create(new ChargeCreateOptions
            {
                Amount =(long)amount*100,
                Currency = "egp",
                Description = "Vechile booking ",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail
            });
            if (change.Status == "succeeded")
            {
                ICarRepository.VehicleBook(new VehicleBooking()
                {
                    HireDate= ICarRepository.ChangeDateFormatToDaysMonthYears(bookingInfo.HireDate),
                    ReturnData=ICarRepository.ChangeDateFormatToDaysMonthYears(bookingInfo.ReturnData),
                    VehicleId=bookingInfo.VehicleId,
                    RenterId=bookingInfo.RenterId
                });
                ICarRepository.Save();

                return RedirectToPage("/PaymentSuccess");
            }
            return RedirectToPage("/PaymentFail");
        }
    }
}