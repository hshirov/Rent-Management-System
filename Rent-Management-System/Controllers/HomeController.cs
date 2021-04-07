using Data;
using Microsoft.AspNetCore.Mvc;
using Rent_Management_System.Models;
using Rent_Management_System.Models.PaymentsModels;
using System;
using System.Diagnostics;
using System.Linq;

namespace Rent_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProperty _properties;
        private readonly ITenant _tenants;
        private readonly IPayment _payments;

        public HomeController(IProperty properties, ITenant tenants, IPayment payments)
        {
            _properties = properties;
            _tenants = tenants;
            _payments = payments;
        }

        public IActionResult Index()
        {
            int month = DateTime.Now.Month;
            HomeViewModel model = new HomeViewModel
            {
                PropertyCount = _properties.GetNumberOfProperties(),
                TenantCount = _tenants.GetNumberOfTenants(),
                RentCollectedThisMonth = _payments.GetAmountFromMonth(month),
                CurrentMonth = DateTime.Now.ToString("MMMM"),
                PaymentsThisMonth = _payments.GetAllFromMonth(month).Select(p => new PaymentItemModel() 
                {
                    TenantId = p.Tenant.Id,
                    TenantName = p.Tenant.FullName,
                    Amount = p.Amount,
                    Date = p.Date.ToString("dd/MM/yyyy")
                })
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
