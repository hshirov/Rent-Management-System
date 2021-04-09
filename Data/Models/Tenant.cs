using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    /// <summary>
    /// Entity that occupies a single property and makes payments
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// Integer, that serves as an identification number for the object 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First name of the tenant
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Name must contain letters only.")]
        [MinLength(2, ErrorMessage = "The minimum length is 2")]
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the tenant
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Name must contain letters only.")]
        [MinLength(2, ErrorMessage = "The minimum length is 2")]
        public string LastName { get; set; }
        /// <summary>
        /// The first and last name of the tenant, combined
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Personal email of the tenant
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Phone number of the tenant
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(10)]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// The date, the tenant moved into the property
        /// </summary>

        [Display(Name = "Date Of Moving In")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfMovingIn { get; set; }
        /// <summary>
        /// If true, tenant is ignored
        /// </summary>
        public bool IsKickedOut { get; set; }
        /// <summary>
        /// The property, the given tenant occupies
        /// </summary>
        [Display(Name = "Rented Property")]
        public virtual Property RentedProperty { get; set; }
        /// <summary>
        /// List of payments, the tenant has made
        /// </summary>
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
