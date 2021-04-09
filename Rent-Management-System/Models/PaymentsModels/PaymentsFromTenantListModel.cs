using System.Collections.Generic;

namespace Rent_Management_System.Models.PaymentsModels
{
    public class PaymentsFromTenantListModel
    {
        public IEnumerable<PaymentItemModel> Payments { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
    }
}
