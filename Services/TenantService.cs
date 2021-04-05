using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class TenantService : ITenant
    {
        private RmsContext _context;
        private IProperty _properties;
        private IPayment _payments;

        public TenantService(RmsContext context, IProperty properties, IPayment payments)
        {
            _context = context;
            _properties = properties;
            _payments = payments;
        }

        public void Add(Tenant tenant)
        {
            tenant.FullName = tenant.FirstName + " " + tenant.LastName;
            _context.Add(tenant);
            _context.SaveChanges();
        }

        public Tenant Get(int id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Tenant> GetAll()
        {
            return _context.Tenants
                .Include(t => t.RentedProperty)
                .Include(t => t.Payments);
        }

        public IEnumerable<Tenant> GetAllFromProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId);
        }

        public double GetMoneyOwed(int tenantId)
        {
            // If there are no payments yet, you still need to pay for the first month
            if(_payments.GetAllFromTenant(tenantId).FirstOrDefault() == null)
            {
                return Get(tenantId).MonthlyRent;
            }

            return Get(tenantId).MonthlyRent * GetMonthsSinceLastPayment(tenantId);
        }

        public double GetMonthlyRent(int tenantId, int propertyId)
        {
            // Split the rent equally between tenants
            return _properties.Get(propertyId).Rent / _properties.GetNumberOfTenants(propertyId);
        }

        public int GetMonthsSinceLastPayment(int tenantId)
        {
            // Check if payments is empty
            if(_payments.GetAllFromTenant(tenantId).FirstOrDefault() == null)
            {
                return 0;
            }

            TimeSpan date = DateTime.Now - Get(tenantId).Payments.Last().Date;
            return date.Days / 30;
        }

        public int GetNumberOfTenants()
        {
            return _context.Tenants.Count();
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
