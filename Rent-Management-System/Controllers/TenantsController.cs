using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Rent_Management_System.Models.TenantModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
                TenantIndexModel model = new TenantIndexModel()
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
            TenantListModel model = new TenantListModel()
            {
                Tenants = _tenants.GetAll().Select(t => new TenantItemModel()
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Address = t.RentedProperty.Address,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    DateOfMovingIn = t.DateOfMovingIn.ToString("dd/MM/yyyy")
                })
            };

            return View(model);
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
                if (_tenants.IsEmailTaken(model.Tenant.Email))
                {
                    ModelState.AddModelError("Tenant.Email", "This email is already taken.");
                    return View(model);
                }

                if (model.Tenant.DateOfMovingIn > DateTime.Now)
                {
                    ModelState.AddModelError("Tenant.DateOfMovingIn", "Date cannot be in the future.");
                    return View(model);
                }

                Tenant newTenant = new Tenant()
                {
                    FirstName = model.Tenant.FirstName,
                    LastName = model.Tenant.LastName,
                    Email = model.Tenant.Email,
                    PhoneNumber = model.Tenant.PhoneNumber,
                    DateOfMovingIn = model.Tenant.DateOfMovingIn
                };

                _tenants.Add(newTenant, model.RentedPropertyId);

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

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _tenants.KickOut(id);

            return RedirectToAction("All");
        }
    }
}
