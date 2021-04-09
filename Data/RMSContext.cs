using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <summary>
    /// RmsContext class
    /// inherits Тhe DbContext
    /// </summary>
    public class RmsContext : DbContext
    {
        /// <summary>
        /// RmsContext
        /// The context for the Database
        /// </summary>
        /// <param name="options"></param>
        public RmsContext(DbContextOptions options) : base(options) { }
        public RmsContext() { }

        /// <summary>
        /// The context Dbset for the Tenants
        /// </summary>
        public virtual DbSet<Tenant> Tenants { get; set; }
        /// <summary>
        /// The context Dbset for the Properties
        /// </summary>
        public virtual DbSet<Property> Properties { get; set; }
        /// <summary>
        /// The context Dbset for the Payments
        /// </summary>
        public virtual DbSet<Payment> Payments { get; set; }
    }
}
