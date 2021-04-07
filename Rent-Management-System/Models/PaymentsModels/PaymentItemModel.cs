namespace Rent_Management_System.Models.PaymentsModels
{
    public class PaymentItemModel
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
    }
}
