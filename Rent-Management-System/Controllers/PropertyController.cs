using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_Management_System.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IProperty _properties;

        public PropertyController(IProperty properties)
        {
            _properties = properties;
        }
        public IActionResult Index()
        {
            IEnumerable<Property> properties = _properties.GetAll();

            return View(properties);
        }
    }
}
