using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    /// <summary>
    /// Property class
    /// Creates the inputs for the Models
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Inputs the Id for the Property class
        /// With 80 letters for maximum length
        /// </summary>
        public int Id { get; set; }
        [StringLength(80)]
        [Required]
        /// <summary>
        /// Inputs the Address for the Property class
        /// With lengh between 1 and 99 letters
        /// </summary>
        public string Address { get; set; }
        [Range(1, 99, ErrorMessage = "Invalid value.")]
        /// <summary>
        /// Inputs the Beds for the Property class
        /// With lengh between 1 and 99 letters
        /// </summary>
        public int Beds { get; set; }
        [Range(1, 99, ErrorMessage = "Invalid value.")]
        /// <summary>
        /// Inputs the Rooms for the Property class
        /// With price between 1 and 99999 Euro
        /// </summary>
        public int Rooms { get; set; }
        [Range(1, 99999, ErrorMessage = "Invalid value.")]
        [Display(Name = "Area In Square Meters")]
        /// <summary>
        /// Inputs the Area for the Area class
        ///? With price between 1 and 99999 Euro?
        /// </summary>
        public int Area { get; set; }
        [Display(Name = "Monthly Rent In Euro")]
        [Range(0, 99999, ErrorMessage = "Invalid value.")]
        /// <summary>
        /// Inputs the Rent for the Area class
        /// </summary>
        public int Rent { get; set; }
        /// <summary>
        /// Inputs the Tenants for the Area class
        /// </summary>
        public virtual IEnumerable<Tenant> Tenants { get; set; }
    }
}
