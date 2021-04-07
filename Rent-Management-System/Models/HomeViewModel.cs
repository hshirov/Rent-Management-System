using System.Collections.Generic;

namespace Rent_Management_System.Models
{
    public class HomeViewModel
    {
        public int TenantCount { get; set; }
        public int PropertyCount { get; set; }
        public string CurrentMonth { get; set; }
        public double RentCollectedThisMonth { get; set; }
        public IEnumerable<PaymentsModels.PaymentItemModel> PaymentsThisMonth { get; set; }
    }
}
