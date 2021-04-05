using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Rent_Management_System.Models;
using System;
using System.Collections.Generic;

namespace Rent_Management_System.Controllers
{
    public class TenantsController : Controller
    {
        private readonly IProperty _properties;
        private readonly ITenant _tenants;
        private readonly IPayment _payments;

        public TenantsController(IProperty properties, ITenant tenants, IPayment payments)
        {
            _properties = properties;
            _tenants = tenants;
            _payments = payments;
        }

        public IActionResult Index(int id)
        {
            Tenant tenant = _tenants.Get(id);
           
            if(tenant != null)
            {
                TenantIndexView model = new TenantIndexView()
                {
                    Tenant = tenant,
                    MoneyOwed = _tenants.GetMoneyOwed(tenant.Id)
                };

                return View(model);
            }

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            IEnumerable<Tenant> tenants = _tenants.GetAll();

            return View(tenants);
        }

        public IActionResult Add()
        {
            Tenant tenant = new Tenant()
            {
                DateOfMovingIn = DateTime.Now
            };

            AddTenantModel model = new AddTenantModel()
            {
                Tenant = tenant,
                Properties = _properties.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddTenantModel model)
        {
            // The value gets set to null on redirect
            model.Properties = _properties.GetAll();

            if (ModelState.IsValid)
            {
                Tenant newTenant = new Tenant()
                {
                    FirstName = model.Tenant.FirstName,
                    LastName = model.Tenant.LastName,
                    Email = model.Tenant.Email,
                    PhoneNumber = model.Tenant.PhoneNumber,
                    DateOfMovingIn = model.Tenant.DateOfMovingIn,
                    RentedProperty = _properties.Get(model.RentedPropertyId),
                    MonthlyRent =  _tenants.GetMonthlyRent(model.Tenant.Id, model.RentedPropertyId)
                };

                _tenants.Add(newTenant);

                return RedirectToAction("All");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult PayRent(int id)
        {
            Payment payment = new Payment()
            {
                Tenant = _tenants.Get(id),
                Amount = _tenants.GetMoneyOwed(id),
                Date = DateTime.Now
            };

            _payments.Add(payment);
            
            return RedirectToAction("Index", new { id = id });
        }
    }
}
