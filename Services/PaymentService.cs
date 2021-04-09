using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    /// <summary>
    /// Handles the payment business logic
    /// </summary>
    public class PaymentService : IPayment
    {
        private RmsContext _context;

        /// <summary>
        /// Constuctor for the Payment Service
        /// </summary>
        /// <param name="context"></param>
        public PaymentService(RmsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new Payment to the Database
        /// </summary>
        /// <param name="payment"></param>
        public void Add(Payment payment)
        {
            _context.Add(payment);
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns a single payment, specified by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Payment Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Returns all payment records ordered by date in descending order
        /// </summary>
        /// <returns>All the Payments</returns>
        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments.Include(p => p.Tenant).OrderByDescending(p => p.Date);
        }

        /// <summary>
        /// All payments from a specific tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public IEnumerable<Payment> GetAllFromTenant(int tenantId)
        {
            return GetAll().Where(p => p.Tenant.Id == tenantId);
        }

        /// <summary>
        /// Get the sum of all payments made by a specific tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public double GetPaymentSum(int tenantId)
        {
            double sum = 0;

            foreach(var payment in GetAllFromTenant(tenantId))
            {
                sum += payment.Amount;
            }

            return sum;
        }

        /// <summary>
        /// Get all payments made on a specific month
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public IEnumerable<Payment> GetAllFromMonth(int month)
        {
            return GetAll().Where(p => p.Date.Month == month);
        }

        /// <summary>
        /// Get sum of payments made on a specific month
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public double GetAmountFromMonth(int month)
        {
            IEnumerable<Payment> payments = GetAllFromMonth(month);

            return GetPaymentsAmount(payments);
        }

        /// <summary>
        /// Get sum of payments made from a specific year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double GetAmountFromYear(int year)
        {
            IEnumerable<Payment> payments = GetAll().Where(p => p.Date.Year == year);

            return GetPaymentsAmount(payments);
        }

        /// <summary>
        /// Get the sum of all payments, regardless of the date
        /// </summary>
        /// <returns></returns>
        public double GetAmountFromAllTime()
        {
            IEnumerable<Payment> payments = GetAll();

            return GetPaymentsAmount(payments);
        }

        /// <summary>
        /// Get the sum of money from a list of payments
        /// </summary>
        /// <param name="payments"></param>
        /// <returns></returns>
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
