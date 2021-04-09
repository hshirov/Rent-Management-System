using Data.Models;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// IProperty interfaces
    /// Handles the functions
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// Interface for the Add function
        /// </summary>
        /// <param name="property"></param>
        void Add(Property property);
        /// <summary>
        /// Interface for the Remoe function by id
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
        /// <summary>
        /// Interface for the Update function by id
        /// </summary>
        /// <param name="property"></param>
        void Update(Property property);
        Property Get(int id);
        IEnumerable<Property> GetAll();
        int GetNumberOfProperties();
    }
}
