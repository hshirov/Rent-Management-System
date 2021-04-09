using Data.Models;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// IPayment interfaces
    /// Handles the functions
    /// </summary>
    public interface IPayment
    {
        /// <summary>
        /// Interface for the function Add
        /// Adds a payment to a certain Tenant
        /// </summary>
        /// <param name="payment"></param>
        void Add(Payment payment);
        /// <summary>
        /// Interface for the function Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Payment via Id</returns>
        Payment Get(int id);
        IEnumerable<Payment> GetAll();
        IEnumerable<Payment> GetAllFromTenant(int tenantId);
        IEnumerable<Payment> GetAllFromMonth(int month);
        /// <summary>
        /// Interface for the function GetPaymentSum
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>The Sum of Payments via tenantId</returns>
        double GetPaymentSum(int tenantId);
        /// <summary>
        /// Interface for the function GetAmountFromMonth
        /// </summary>
        /// <param name="month"></param>
        /// <returns>The Payment for the month</returns>
        double GetAmountFromMonth(int month);
        /// <summary>
        /// Interface for the function GetAmountFromYear
        /// </summary>
        /// <param name="year"></param>
        /// <returns>The Payment for the whole year</returns>
        double GetAmountFromYear(int year);
        /// <summary>
        /// Interface for the function GetAmountFromAllTime
        /// </summary>
        /// <returns>All the Payments</returns>
        double GetAmountFromAllTime();
    }
}
