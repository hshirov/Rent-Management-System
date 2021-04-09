using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    /// <summary>
    /// The PropertyService Class
    /// </summary>
    public class PropertyService : IProperty
    {
        private RmsContext _context;
        /// <summary>
        /// Constuctor for the class Property Service
        /// </summary>
        /// <param name="context"></param>
        public PropertyService(RmsContext context)
        {
            _context = context;
        }
        /// <summary>
        /// The Add Function - Adds a new property
        /// </summary>
        /// <param name="property"></param>
        public void Add(Property property)
        {
            _context.Add(property);
            _context.SaveChanges();
        }
        /// <summary>
        /// The Get Function
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The specified Property via ID</returns>
        public Property Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }
        /// <summary>
        /// The GetAll Function
        /// </summary>
        /// <returns>All the Properties</returns>
        public IEnumerable<Property> GetAll()
        {
            return _context.Properties.Include(p => p.Tenants);
        }
        /// <summary>
        /// GetNumberOfProperties Function
        /// </summary>
        /// <returns>The amount of all properties</returns>
        public int GetNumberOfProperties()
        {
            return _context.Properties.Count();
        }
        /// <summary>
        /// Remove Function - Removes a certain property via ID
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            _context.Properties.Remove(Get(id));
            _context.SaveChanges();
        }
        /// <summary>
        /// The Update Function - Edits a certain property
        /// </summary>
        /// <param name="property"></param>
        public void Update(Property property)
        {
            Property entityToUpdate = Get(property.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(property);

            _context.SaveChanges();
        }
    }
}
