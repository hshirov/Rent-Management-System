using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class PropertyService : IProperty
    {
        private RmsContext _context;

        public PropertyService(RmsContext context)
        {
            _context = context;
        }

        public void Add(Property property)
        {
            _context.Add(property);

            _context.SaveChanges();
        }

        public Property Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Property> GetAll()
        {
            return _context.Properties.Include(p => p.Tenants);
        }

        public int GetNumberOfProperties()
        {
            return _context.Properties.Count();
        }

        public void Remove(int id)
        {
            _context.Properties.Remove(Get(id));

            _context.SaveChanges();
        }

        public void Update(Property property)
        {
            Property entityToUpdate = Get(property.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(property);

            _context.SaveChanges();
        }
    }
}
