using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Rent_Management_System.Models;
using System;

namespace Rent_Management_System.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IPayment _payments;
        private readonly ITenant _tenants;

        public PaymentsController(IPayment payments, ITenant tenants)
        {
            _payments = payments;
            _tenants = tenants;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            return View(_payments.GetAll());
        }

        public IActionResult Tenant(int id)
        {
            return View(_payments.GetAllFromTenant(id));
        }

        public IActionResult Add()
        {
            Payment payment = new Payment()
            {
                Date = DateTime.Now
            };

            AddPaymentModel model = new AddPaymentModel()
            {
                Payment = payment,
                Tenants = _tenants.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddPaymentModel model)
        {
            // The value gets set to null on redirect
            model.Tenants = _tenants.GetAll();

            if (ModelState.IsValid)
            {
                Payment newPayment = new Payment()
                {
                    Tenant = _tenants.Get(model.TenantId),
                    Amount = model.Payment.Amount,
                    Date = model.Payment.Date
                };

                _payments.Add(newPayment);

                return RedirectToAction("All", "Payments");
            }

            return View(model);
        }
    }
}
