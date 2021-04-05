using Data.Models;

namespace Rent_Management_System.Models
{
    public class TenantIndexView
    {
        public Tenant Tenant { get; set; }
        public double MoneyOwed { get; set; }
    }
}
