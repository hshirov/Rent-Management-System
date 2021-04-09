using Data.Models;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// ITenant interfaces
    /// Handles the functions
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// Adds tenat and rentedPropertyId
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="rentedPropertyId"></param>
        void Add(Tenant tenant, int rentedPropertyId);
        /// <summary>
        /// Kicks out tenant by id
        /// </summary>
        /// <param name="id"></param>
        void KickOut(int id);
        /// <summary>
        /// Updates Tenat id, propertyId, tenantId, email
        /// </summary>
        /// <param name="tenant"></param>
        void Update(Tenant tenant);
        Tenant Get(int id);
        IEnumerable<Tenant> GetAll();
        IEnumerable<Tenant> GetAllFromProperty(int propertyId);
        int GetNumberOfTenants();
        int GetNumberOfTenantsInProperty(int propertyId);
        double GetMoneyOwed(int tenantId);
        bool IsEmailTaken(string email);
        bool HasPayments(int id);
    }
}
