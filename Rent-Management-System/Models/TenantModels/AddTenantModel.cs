using Data.Models;
using System.Collections.Generic;

namespace Rent_Management_System.Models.TenantModels
{
    public class AddTenantModel
    {
        public Tenant Tenant { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public int RentedPropertyId { get; set; }
    }
}
