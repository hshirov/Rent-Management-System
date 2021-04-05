using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Tenant Tenant { get; set; }
        [Required]
        [Display(Name = "Amount In Euros")]
        [Range(1, 999999, ErrorMessage = "Invalid value.")]
        public double Amount { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
