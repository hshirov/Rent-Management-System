using Data.Models;
using System.Collections.Generic;

namespace Rent_Management_System.Models.PropertyModels
{
    public class PropertyIndexModel
    {
        public string Address { get; set; }
        public int Area { get; set; }
        public int Rooms { get; set; }
        public int Beds { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
    }
}
