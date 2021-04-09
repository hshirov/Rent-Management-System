using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    /// <summary>
    /// The object tenants are assigned to and get their monthly rent decided by
    /// A single property can have multiple tenants
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Integer, that serves as an identification number for the object 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the address of the property (Location)
        /// </summary>
        [StringLength(80)]
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// Number of beds for the given property
        /// </summary>
        [Range(1, 99, ErrorMessage = "Invalid value.")]
        public int Beds { get; set; }
        /// <summary>
        /// Number of rooms for the given property
        /// </summary>
        [Range(1, 99, ErrorMessage = "Invalid value.")]
        public int Rooms { get; set; }
        /// <summary>
        /// Area of the property, mesured in square meters
        /// </summary>
        [Range(1, 99999, ErrorMessage = "Invalid value.")]
        [Display(Name = "Area In Square Meters")]
        public int Area { get; set; }
        /// <summary>
        /// The monthly rent of the property
        /// </summary>
        [Display(Name = "Monthly Rent In Euro")]
        [Range(0, 99999, ErrorMessage = "Invalid value.")]
        public int Rent { get; set; }
        /// <summary>
        /// Reference to a list of tenants, that live in the property
        /// </summary>
        public virtual IEnumerable<Tenant> Tenants { get; set; }
    }
}
