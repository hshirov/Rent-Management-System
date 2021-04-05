using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class PaymentService : IPayment
    {
        private RmsContext _context;

        public PaymentService(RmsContext context)
        {
            _context = context;
        }

        public void Add(Payment payment)
        {
            _context.Add(payment);
            _context.SaveChanges();
        }

        public Payment Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments.Include(p => p.Tenant);
        }

        public IEnumerable<Payment> GetAllFromTenant(int tenantId)
        {
            return GetAll().Where(p => p.Tenant.Id == tenantId);
        }

        public double GetPaymentSum(int tenantId)
        {
            double sum = 0;

            foreach(var payment in GetAllFromTenant(tenantId))
            {
                sum += payment.Amount;
            }

            return sum;
        }
    }
}
