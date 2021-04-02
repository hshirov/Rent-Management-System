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

        public TenantsController(IProperty properties, ITenant tenants)
        {
            _properties = properties;
            _tenants = tenants;
        }

        public IActionResult Index()
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
                    Owed = 0,
                    Paid = 0,
                    RentedProperty = _properties.Get(model.RentedPropertyId)
                };

                _tenants.Add(newTenant);

                return RedirectToAction("Index", "Tenants");
            }

            return View(model);
        }
    }
}
