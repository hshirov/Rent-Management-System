using Data.Models;
using System.Collections.Generic;

namespace Data
{
    public interface IProperty
    {
        void Add(Property property);
        void Remove(int id);
        void Update(Property property);
        Property Get(int id);
        IEnumerable<Property> GetAll();
        int GetNumberOfProperties();
        int GetNumberOfTenants(int propertyId);
    }
}
