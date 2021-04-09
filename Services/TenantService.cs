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
    /// The TenantService class
    /// Manages the actions for the Tenants
    /// </summary>
    public class TenantService : ITenant
    {
        private RmsContext _context;
        private IProperty _properties;
        private IPayment _payments;
        /// <summary>
        /// Constructor for the class TenantService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="properties"></param>
        /// <param name="payments"></param>
        public TenantService(RmsContext context, IProperty properties, IPayment payments)
        {
            _context = context;
            _properties = properties;
            _payments = payments;
        }
        /// <summary>
        /// The Add Function
        /// Adds a new Tenant to the Database
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="rentedPropertyId"></param>
        public void Add(Tenant tenant, int rentedPropertyId)
        {
            tenant.FirstName = StringManipulation.NormalizeName(tenant.FirstName);
            tenant.LastName = StringManipulation.NormalizeName(tenant.LastName);
            tenant.FullName = tenant.FirstName + " " + tenant.LastName;

            tenant.RentedProperty = _properties.Get(rentedPropertyId);
            tenant.MonthlyRent = CalculateMonthlyRent(tenant.Id, rentedPropertyId);
            tenant.IsKickedOut = false;

            _context.Add(tenant);
            _context.SaveChanges();
        }
        /// <summary>
        /// Get Function
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The specified Tenant via ID</returns>
        public Tenant Get(int id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }
        /// <summary>
        /// GetAll Function
        /// </summary>
        /// <returns>All the Tenants</returns>
        public IEnumerable<Tenant> GetAll()
        {
            return _context.Tenants
                .Where(t => t.IsKickedOut == false)
                .Include(t => t.RentedProperty)
                .Include(t => t.Payments);
        }
        /// <summary>
        /// GetAllFromProperty Function
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns>All the Tenants from  a specific Property via propertyID</returns>
        public IEnumerable<Tenant> GetAllFromProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId && !t.IsKickedOut);
        }
        /// <summary>
        /// GetMoneyOwed Function
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>Returns the amount of money a certain Tenant owes</returns>
        public double GetMoneyOwed(int tenantId)
        {
            int factor = GetMonthsSinceMovingIn(tenantId);

            // If the tenent has no previous payments, get the date of moving in
            if (HasPayments(tenantId))
            {
                factor = GetMonthsSinceLastPayment(tenantId);
            }

            return Get(tenantId).MonthlyRent * factor;
        }
        /// <summary>
        /// GetNumberOFTenants Function
        /// </summary>
        /// <returns>The number of all Tenants</returns>
        public int GetNumberOfTenants()
        {
            return _context.Tenants.Where(t => !t.IsKickedOut).Count();
        }
        /// <summary>
        /// IsEmailTaken
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Whether a certain E-mail is taken or not</returns>
        public bool IsEmailTaken(string email)
        {
            return GetAll().Any(t => t.Email == email);
        }
        /// <summary>
        /// GetNumberOfTenantsInProperty Function
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns>The number of Tenants in a certain Property via PropertyID</returns>
        public int GetNumberOfTenantsInProperty(int propertyId)
        {
            return GetAll().Where(t => t.RentedProperty.Id == propertyId).Count();
        }
        /// <summary>
        /// HasPayments Function
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>Whether a certain Tenant has any Payments</returns>
        public bool HasPayments(int tenantId)
        {
            if (Get(tenantId) == null || _payments.GetAllFromTenant(tenantId).FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// KickOut Function
        /// Removes a Tenant via Id from the Database
        /// </summary>
        /// <param name="id"></param>
        public void KickOut(int id)
        {
            Tenant tenant = Get(id);
            _context.Update(tenant);

            tenant.IsKickedOut = true;

            _context.SaveChanges();
        }
        /// <summary>
        /// Update Function
        ///  Edits a Tenant's data from the Database
        /// </summary>
        /// <param name="tenant"></param>
        public void Update(Tenant tenant)
        {
            Tenant entityToUpdate = Get(tenant.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(tenant);

            _context.SaveChanges();
        }
        /// <summary>
        /// GetMonthsSinceLastPayment
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>The number of months since last Payment for a certain Tenant</returns>
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
        /// GetMonthsSinceMovingIn
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>The number of months since the Tenant has moved in</returns>
        private int GetMonthsSinceMovingIn(int tenantId)
        {
            return (int)(DateTime.Now - Get(tenantId).DateOfMovingIn).TotalDays / 30;
        }
        /// <summary>
        /// CalculateMonthlyRent
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="propertyId"></param>
        /// <returns>The sum a certain Tenants owes for the month</returns>
        private double CalculateMonthlyRent(int tenantId, int propertyId)
        {
            // Split the rent equally between tenants
            int numberOfTenants = GetNumberOfTenantsInProperty(propertyId) + 1;

            if (numberOfTenants > 0)
            {
                return _properties.Get(propertyId).Rent / numberOfTenants;
            }

            return _properties.Get(propertyId).Rent;
        }
    }
}
