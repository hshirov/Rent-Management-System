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

        public void Add(Tenant tenant, int rentedPropertyId)
        {
            tenant.FullName = tenant.FirstName + " " + tenant.LastName;
            tenant.RentedProperty = _properties.Get(rentedPropertyId);
            tenant.MonthlyRent = GetMonthlyRent(tenant.Id, rentedPropertyId);
            tenant.IsKickedOut = false;

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
                .Where(t => t.IsKickedOut == false)
                .Include(t => t.RentedProperty)
                .Include(t => t.Payments);
        }

        public IEnumerable<Tenant> GetAllFromProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId && !t.IsKickedOut);
        }

        public double GetMoneyOwed(int tenantId)
        {
            int factor = GetMonthsSinceMovingIn(tenantId);

            // If the tenent has no previous payments, get the date of moving in
            if (_payments.GetAllFromTenant(tenantId).FirstOrDefault() != null)
            {
                factor = GetMonthsSinceLastPayment(tenantId);
            }

            return GetMonthlyRent(tenantId, Get(tenantId).RentedProperty.Id) * factor;
        }

        public double GetMonthlyRent(int tenantId, int propertyId)
        {
            // Split the rent equally between tenants
            int numberOfTenants = GetNumberOfTenantsInProperty(propertyId);

            if(numberOfTenants > 0)
            {
                return _properties.Get(propertyId).Rent / numberOfTenants;
            }

            return _properties.Get(propertyId).Rent;
        }

        public int GetNumberOfTenants()
        {
            return _context.Tenants.Where(t => !t.IsKickedOut).Count();
        }

        public bool IsEmailTaken(string email)
        {
            return GetAll().Any(t => t.Email == email);
        }

        public void KickOut(int id)
        {
            Tenant tenant = Get(id);
            _context.Update(tenant);

            tenant.IsKickedOut = true;

            _context.SaveChanges();
        }

        public void Update(Tenant tenant)
        {
            Tenant entityToUpdate = Get(tenant.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(tenant);

            _context.SaveChanges();
        }

        private int GetMonthsSinceLastPayment(int tenantId)
        {
            // Check if payments is empty
            if (_payments.GetAllFromTenant(tenantId).FirstOrDefault() == null)
            {
                return 0;
            }

            TimeSpan date = DateTime.Now - Get(tenantId).Payments.Last().Date;
            return date.Days / 30;
        }

        private int GetMonthsSinceMovingIn(int tenantId)
        {
            return (int)(DateTime.Now - Get(tenantId).DateOfMovingIn).TotalDays / 30;
        }

        public int GetNumberOfTenantsInProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId).Count();
        }

        public bool HasPayments(int id)
        {
            if(Get(id) == null || Get(id).Payments.FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }
    }
}
