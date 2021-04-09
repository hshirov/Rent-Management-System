using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <summary>
    /// Database context of the application
    /// </summary>
    public class RmsContext : DbContext
    {
        public RmsContext(DbContextOptions options) : base(options) { }
        public RmsContext() { }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
    }
}
