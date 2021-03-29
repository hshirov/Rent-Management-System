using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RMSContext : DbContext
    {
        public RMSContext(DbContextOptions options) : base(options) { }
        public RMSContext() { }

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
    }
}
