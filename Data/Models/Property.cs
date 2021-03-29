using System.Collections.Generic;

namespace Data.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LivingCapacity { get; set; }
        public int Rent { get; set; }
        public virtual IEnumerable<Tenant> Tenants { get; set; }
    }
}
