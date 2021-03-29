using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class TenantService : ITenant
    {
        private RMSContext _context;

        public TenantService(RMSContext context)
        {
            _context = context;
        }

        public void Add(Tenant tenant)
        {
            _context.Add(tenant);

            _context.SaveChanges();
        }

        public Tenant Get(int id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Tenant> GetAll()
        {
            return _context.Tenants.Include(t => t.RentedProperty);
        }

        public IEnumerable<Tenant> GetAllFromProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId);
        }

        public void Remove(int id)
        {
            _context.Tenants.Remove(Get(id));

            _context.SaveChanges();
        }

        public void Update(Tenant tenant)
        {
            Tenant entityToUpdate = Get(tenant.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(tenant);

            _context.SaveChanges();
        }
    }
}
