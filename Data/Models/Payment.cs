using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    /// <summary>
    /// Used to document the transactions made from tenants, i.e., paying rent
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Integer, that serves as an identification number for the object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The tenant who made the payment
        /// </summary>
        public Tenant Tenant { get; set; }
        /// <summary>
        /// The amount of money given by the tenant
        /// </summary>
        [Required]
        [Display(Name = "Amount In Euros")]
        [Range(1, 999999, ErrorMessage = "Invalid value.")]
        public double Amount { get; set; }
        /// <summary>
        /// Date of the transaction
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
