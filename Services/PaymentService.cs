using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    /// <summary>
    /// Payment Service class
    /// Manages the actions for the Payments
    /// </summary>
    public class PaymentService : IPayment
    {
        private RmsContext _context;
        /// <summary>
        /// Constuctor for the class Payment Service
        /// </summary>
        /// <param name="context"></param>
        public PaymentService(RmsContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Add function
        /// Adds a new Payment to the Database
        /// </summary>
        /// <param name="payment"></param>
        public void Add(Payment payment)
        {
            _context.Add(payment);
            _context.SaveChanges();
        }
        /// <summary>
        /// Get Function
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The specified Payment via ID</returns>
        public Payment Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }
        /// <summary>
        /// GetAll Function
        /// </summary>
        /// <returns>All the Payments</returns>
        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments.Include(p => p.Tenant).OrderByDescending(p => p.Date);
        }
        /// <summary>
        /// GetAllFromTenant Function
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>All the Payments of the specified tenant via ID</returns>
        public IEnumerable<Payment> GetAllFromTenant(int tenantId)
        {
            return GetAll().Where(p => p.Tenant.Id == tenantId);
        }
        /// <summary>
        /// GetPaymentSum
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>Sum of all the Payments</returns>
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
        /// GetAllFromMonth Function
        /// </summary>
        /// <param name="month"></param>
        /// <returns>The number of all the Payments from the last month</returns>
        public IEnumerable<Payment> GetAllFromMonth(int month)
        {
            return GetAll().Where(p => p.Date.Month == month);
        }
        /// <summary>
        /// GetAmountFromMonth Function
        /// </summary>
        /// <param name="month"></param>
        /// <returns>Specified Payment from the last month</returns>
        public double GetAmountFromMonth(int month)
        {
            IEnumerable<Payment> payments = GetAllFromMonth(month);

            return GetPaymentsAmount(payments);
        }
        /// <summary>
        /// GetAmountFromYear Function
        /// </summary>
        /// <param name="year"></param>
        /// <returns>The Number of all the Payments from the last year</returns>
        public double GetAmountFromYear(int year)
        {
            IEnumerable<Payment> payments = GetAll().Where(p => p.Date.Year == year);

            return GetPaymentsAmount(payments);
        }
        /// <summary>
        /// GetAmountFromAllTime
        /// </summary>
        /// <returns>All the Payments since the beginning</returns>
        public double GetAmountFromAllTime()
        {
            IEnumerable<Payment> payments = GetAll();

            return GetPaymentsAmount(payments);
        }
        /// <summary>
        /// GetPaymentsAmount
        /// </summary>
        /// <param name="payments"></param>
        /// <returns>The number of Payments</returns>
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
