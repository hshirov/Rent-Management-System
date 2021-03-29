using Data;
using Microsoft.AspNetCore.Mvc;
using Rent_Management_System.Models;
using System.Diagnostics;

namespace Rent_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProperty _properties;
        private readonly ITenant _tenants;

        public HomeController(IProperty properties, ITenant tenants)
        {
            _properties = properties;
            _tenants = tenants;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
