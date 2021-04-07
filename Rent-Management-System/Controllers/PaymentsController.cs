using Data;
using Microsoft.AspNetCore.Mvc;
using Rent_Management_System.Models.PaymentsModels;
using System.Linq;

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
            PaymentListModel model = new PaymentListModel()
            {
                Payments = _payments.GetAll().Select(t => new PaymentItemModel()
                {
                    TenantId = t.Tenant.Id,
                    TenantName = t.Tenant.FirstName,
                    Amount = t.Amount,
                    Date = t.Date.ToString("dd/MM/yyyy")
                })
            };

            return View(model);
        }

        public IActionResult Tenant(int id)
        {
            if (!_tenants.HasPayments(id))
            {
                return RedirectToAction("All");
            }

            PaymentListModel model = new PaymentListModel()
            {
                Payments = _payments.GetAllFromTenant(id).Select(t => new PaymentItemModel()
                {
                    TenantId = t.Tenant.Id,
                    TenantName = t.Tenant.FirstName,
                    Amount = t.Amount,
                    Date = t.Date.ToString("dd/MM/yyyy")
                })
            };

            return View(model);
        }
    }
}
