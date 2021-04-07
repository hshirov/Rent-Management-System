using Data.Models;
using System.Collections.Generic;

namespace Data
{
    public interface ITenant
    {
        void Add(Tenant tenant, int rentedPropertyId);
        void KickOut(int id);
        void Update(Tenant tenant);
        Tenant Get(int id);
        IEnumerable<Tenant> GetAll();
        IEnumerable<Tenant> GetAllFromProperty(int propertyId);
        int GetNumberOfTenants();
        int GetNumberOfTenantsInProperty(int propertyId);
        double GetMonthlyRent(int tenantId, int propertyId);
        double GetMoneyOwed(int tenantId);
        bool IsEmailTaken(string email);
    }
}
