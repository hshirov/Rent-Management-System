using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    /// <summary>
    /// Handles the property business logic
    /// </summary>
    public class PropertyService : IProperty
    {
        private RmsContext _context;

        /// <summary>
        /// Constuctor for the Property Service
        /// </summary>
        /// <param name="context">Database context</param>
        public PropertyService(RmsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new property to the Database
        /// </summary>
        /// <param name="property"></param>
        public void Add(Property property)
        {
            _context.Add(property);
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns a single property, specified by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A property matching the id or null if none matches</returns>
        public Property Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Returns all property records
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Property> GetAll()
        {
            return _context.Properties.Include(p => p.Tenants);
        }

        /// <summary>
        /// Gets the number of all properties
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfProperties()
        {
            return _context.Properties.Count();
        }

        /// <summary>
        /// Update the values of a single property
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
