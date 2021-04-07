using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Rent_Management_System.Models.PropertyModels;
using Rent_Management_System.Models.TenantModels;
using System.Linq;

namespace Rent_Management_System.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IProperty _properties;
        private readonly ITenant _tenants;

        public PropertyController(IProperty properties, ITenant tenants)
        {
            _properties = properties;
            _tenants = tenants;
        }

        public IActionResult Index(int id)
        {
            Property property = _properties.Get(id);

            PropertyIndexModel model = new PropertyIndexModel()
            {
                Address = property.Address,
                Area = property.Area,
                Rooms = property.Rooms,
                Beds = property.Beds,
                Rent = property.Rent,
                Tenants = _tenants.GetAllFromProperty(id).Select(t => new TenantItemModel() 
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

        public IActionResult All()
        {
            PropertyListModel model = new PropertyListModel()
            {
                Properties = _properties.GetAll().Select(p => new PropertyItemModel
                {
                    Id = p.Id,
                    Address = p.Address,
                    NumberOfTenants = _tenants.GetAllFromProperty(p.Id).Count()                
                })
            };

            return View(model);
        }

        public IActionResult Add()
        {
            Property model = new Property()
            {
                Rooms = 1,
                Beds = 1,
                Area = 60
            };       

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Property model)
        {
            if (ModelState.IsValid)
            {
                _properties.Add(model);

                return RedirectToAction("All", "Property");
            }

            return View(model);
        }
    }
}
