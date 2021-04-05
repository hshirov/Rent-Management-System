using Data.Models;
using System.Collections.Generic;

namespace Rent_Management_System.Models
{
    public class AddPaymentModel
    {
        public Payment Payment { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
        public int TenantId { get; set; }
    }
}
