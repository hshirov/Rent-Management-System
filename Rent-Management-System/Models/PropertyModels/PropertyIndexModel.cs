using System.Collections.Generic;

namespace Rent_Management_System.Models.PropertyModels
{
    public class PropertyIndexModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Area { get; set; }
        public int Rooms { get; set; }
        public int Beds { get; set; }
        public double Rent { get; set; }
        public IEnumerable<TenantModels.TenantItemModel> Tenants { get; set; }
    }
}
