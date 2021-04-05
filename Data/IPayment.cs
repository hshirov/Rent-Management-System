using Data.Models;
using System.Collections.Generic;

namespace Data
{
    public interface IPayment
    {
        void Add(Payment payment);
        Payment Get(int id);
        IEnumerable<Payment> GetAll();
        IEnumerable<Payment> GetAllFromTenant(int tenantId);
        double GetPaymentSum(int tenantId);
    }
}
