using System.Collections.Generic;

namespace Rent_Management_System.Models.PaymentsModels
{
    public class PaymentListModel
    {
        public IEnumerable<PaymentItemModel> Payments { get; set; }
    }
}
