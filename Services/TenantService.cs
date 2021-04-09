using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Services.Common;

namespace Services
{
    /// <summary>
    /// Handles most of the business logic, that has to do with the tenant
    /// </summary>
    public class TenantService : ITenant
    {
        private RmsContext _context;
        private IProperty _properties;
        private IPayment _payments;

        /// <summary>
        /// Constructor for the class
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="properties">Property service</param>
        /// <param name="payments">Payment service</param>
        public TenantService(RmsContext context, IProperty properties, IPayment payments)
        {
            _context = context;
            _properties = properties;
            _payments = payments;
        }

        /// <summary>
        /// Adds a new Tenant and links him to a property
        /// </summary>
        /// <param name="tenant">Tenant object</param>
        /// <param name="rentedPropertyId">Id for a rented property instance</param>
        public void Add(Tenant tenant, int rentedPropertyId)
        {
            tenant.FirstName = StringManipulation.NormalizeName(tenant.FirstName);
            tenant.LastName = StringManipulation.NormalizeName(tenant.LastName);
            tenant.FullName = tenant.FirstName + " " + tenant.LastName;

            tenant.RentedProperty = _properties.Get(rentedPropertyId);
            tenant.IsKickedOut = false;

            _context.Add(tenant);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get a single tenant by id
        /// </summary>
        /// <param name="id">Tenant id</param>
        /// <returns>Tenant with the corresponding id or null if not found</returns>
        public Tenant Get(int id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Get all tenant records, except for the ones kicked out
        /// </summary>
        /// <returns>All tenants</returns>
        public IEnumerable<Tenant> GetAll()
        {
            return _context.Tenants
                .Where(t => !t.IsKickedOut)
                .Include(t => t.RentedProperty)
                .Include(t => t.Payments);
        }

        /// <summary>
        /// Get all tenant records from a specific property
        /// </summary>
        /// <param name="propertyId">Id of the property</param>
        /// <returns>All tenants found</returns>
        public IEnumerable<Tenant> GetAllFromProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId);
        }

        /// <summary>
        /// Calculate the debt of a single tenant
        /// </summary>
        /// <param name="tenantId">Id of the tenant</param>
        /// <returns>The amount of money owed</returns>
        public double GetMoneyOwed(int tenantId)
        {
            int factor = GetMonthsSinceMovingIn(tenantId);

            // If the tenent has no previous payments, get the date of moving in
            if (HasPayments(tenantId))
            {
                factor = GetMonthsSinceLastPayment(tenantId);
            }

            return CalculateMonthlyRent(tenantId) * factor;
        }

        /// <summary>
        /// Get the number of all tenant records
        /// </summary>
        /// <returns>Tenant count</returns>
        public int GetNumberOfTenants()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// Checks if there is a tenant with the given email
        /// </summary>
        /// <param name="email">The email being checked</param>
        /// <returns></returns>
        public bool IsEmailTaken(string email)
        {
            return GetAll().Any(t => t.Email == email);
        }
        /// <summary>
        /// Get the number of all tenant records that occupy a given property
        /// </summary>
        /// <param name="propertyId">Id of the property</param>
        /// <returns>The number of tenants</returns>
        public int GetNumberOfTenantsInProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId).Count();
        }

        /// <summary>
        /// Check if a user has made any payments
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public bool HasPayments(int tenantId)
        {
            if (Get(tenantId) == null || _payments.GetAllFromTenant(tenantId).FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Set IsKickedOut to true
        /// </summary>
        /// <param name="id">Id of user getting kicked out</param>
        public void KickOut(int id)
        {
            Tenant tenant = Get(id);
            _context.Update(tenant);

            tenant.IsKickedOut = true;

            _context.SaveChanges();
        }

        /// <summary>
        /// Update tenant data
        /// </summary>
        /// <param name="tenant"></param>
        public void Update(Tenant tenant)
        {
            Tenant entityToUpdate = Get(tenant.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(tenant);

            _context.SaveChanges();
        }

        /// <summary>
        /// Get the number of months passed since a tenant's last payment
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the number of months passed since a tenant moved into a property
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private int GetMonthsSinceMovingIn(int tenantId)
        {
            return (int)(DateTime.Now - Get(tenantId).DateOfMovingIn).TotalDays / 30;
        }

        /// <summary>
        /// Divide the rent of the property equally between the tenants
        /// </summary>
        /// <param name="tenantId">Id of the tenant beign evaluated</param>
        /// <returns></returns>
        private double CalculateMonthlyRent(int tenantId)
        {
            int propertyId = Get(tenantId).RentedProperty.Id;
            return _properties.Get(propertyId).Rent / GetNumberOfTenantsInProperty(propertyId);
        }
    }
}
