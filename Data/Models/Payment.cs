using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    /// <summary>
    /// Payment class
    /// Creates the inputs for the Models
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Inputs the Id for the Payment class
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Inputs the Tenant for the Payment class
        /// </summary>
        public Tenant Tenant { get; set; }
        [Required]
        [Display(Name = "Amount In Euros")]
        [Range(1, 999999, ErrorMessage = "Invalid value.")]
        /// <summary>
        /// Sets the criteria for the Sum 
        /// Inputs the Amount for the Payment class
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Sets the Date Format
        /// Inputs the Date for the Payment class
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        /// <summary>
        /// Inputs the Date for the Payment class
        /// </summary>
        public DateTime Date { get; set; }
    }
}
