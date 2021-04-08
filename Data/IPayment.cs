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
        IEnumerable<Payment> GetAllFromMonth(int month);
        double GetPaymentSum(int tenantId);
        double GetAmountFromMonth(int month);
        double GetAmountFromYear(int year);
        double GetAmountFromAllTime();
    }
}
