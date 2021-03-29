using System.Collections.Generic;

namespace Data.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Beds { get; set; }
        public int Rooms { get; set; }
        public int Area { get; set; }
        public int Rent { get; set; }
        public virtual IEnumerable<Tenant> Tenants { get; set; }
    }
}
