using Data.Models;
using System.Collections.Generic;

namespace Data
{
    public interface ITenant
    {
        void Add(Tenant tenant);
        void Remove(int id);
        void Update(Tenant tenant);
        Tenant Get(int id);
        IEnumerable<Tenant> GetAll();
        IEnumerable<Tenant> GetAllFromProperty(int propertyId);
        int GetNumberOfTenants();
        double GetMonthlyRent(int tenantId, int propertyId);
        double GetMoneyOwed(int tenantId);
    }
}
