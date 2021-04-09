using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    /// <summary>
    /// Tenant class
    /// Creates the inputs for the Models
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// Inputs the Id for the Payment class
        /// Require First name
        /// The name must contains only letters and must be at least 2 letters long
        /// </summary>
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Name must contain letters only.")]
        [MinLength(2, ErrorMessage = "The minimum length is 2")]
        /// <summary>
        /// Inputs the FirstName for the Payment class
        /// Require Last name
        /// The name must contains only letters and must be at least 2 letters long
        /// </summary>
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Name must contain letters only.")]
        [MinLength(2, ErrorMessage = "The minimum length is 2")]
        /// <summary>
        /// Inputs the LastName for the Payment class
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Inputs the FullName for the Payment class
        /// Require Email
        /// Valid E-mail
        /// </summary>
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        /// <summary>
        /// Inputs the Email for the Payment class
        /// Require Phone number
        /// The numbr lengh must be 10 numbers
        /// </summary>
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(10)]
        [Phone]
        [Display(Name = "Phone Number")]
        /// <summary>
        /// Inputs the PhoneNumber for the Payment class
        /// Sets the date of moving in
        /// </summary>
        public string PhoneNumber { get; set; }

        [Display(Name = "Date Of Moving In")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        /// <summary>
        /// Inputs the DateOfMovingIn for the Payment class
        /// </summary>
        public DateTime DateOfMovingIn { get; set; }
        /// <summary>
        /// Inputs the MonthlyRent for the Payment class
        /// </summary>
        public double MonthlyRent { get; set; }
        /// <summary>
        /// Inputs the MoneyOwed for the Payment class
        /// </summary>
        public double MoneyOwed { get; set; }
        /// <summary>
        /// Inputs the IsKickedOut for the Payment class
        /// Shows Rented Property
        /// </summary>
        public bool IsKickedOut { get; set; }
        [Display(Name = "Rented Property")]
        /// <summary>
        /// Inputs the RentedProperty for the Payment class
        /// </summary>
        public virtual Property RentedProperty { get; set; }
        /// <summary>
        /// Inputs the Payments for the Payment class
        /// </summary>
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
