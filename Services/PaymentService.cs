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
            return _context.Payments.Include(p => p.Tenant).OrderByDescending(p => p.Date);
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

        public IEnumerable<Payment> GetAllFromMonth(int month)
        {
            return GetAll().Where(p => p.Date.Month == month);
        }

        public double GetAmountFromMonth(int month)
        {
            IEnumerable<Payment> payments = GetAllFromMonth(month);

            return GetPaymentsAmount(payments);
        }

        public double GetAmountFromYear(int year)
        {
            IEnumerable<Payment> payments = GetAll().Where(p => p.Date.Year == year);

            return GetPaymentsAmount(payments);
        }

        public double GetAmountFromAllTime()
        {
            IEnumerable<Payment> payments = GetAll();

            return GetPaymentsAmount(payments);
        }

        private double GetPaymentsAmount(IEnumerable<Payment> payments)
        {
            double sum = 0;

            foreach (var payment in payments)
            {
                sum += payment.Amount;
            }

            return sum;
        }
    }
}
