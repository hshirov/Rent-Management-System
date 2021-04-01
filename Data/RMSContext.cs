using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RmsContext : DbContext
    {
        public RmsContext(DbContextOptions options) : base(options) { }
        public RmsContext() { }

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
    }
}
