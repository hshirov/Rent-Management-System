using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Rent_Management_System.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IProperty _properties;

        public PropertyController(IProperty properties)
        {
            _properties = properties;
        }

        public IActionResult Index(int id)
        {
            Property model = _properties.Get(id);

            return View(model);
        }

        public IActionResult All()
        {
            IEnumerable<Property> properties = _properties.GetAll();
            return View(properties);
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
