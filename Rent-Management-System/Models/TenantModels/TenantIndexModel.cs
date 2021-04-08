namespace Rent_Management_System.Models.TenantModels
{
    public class TenantIndexModel
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantAddress { get; set; }
        public string DateOfMovingIn { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double MoneyOwed { get; set; }
    }
}
