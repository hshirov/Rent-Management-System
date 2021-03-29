using System;
namespace Data.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfMovingIn { get; set; }
        public int Owed { get; set; }
        public int Paid { get; set; }
        public virtual Property RentedProperty { get; set; }
    }
}
