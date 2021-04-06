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
    }
}
