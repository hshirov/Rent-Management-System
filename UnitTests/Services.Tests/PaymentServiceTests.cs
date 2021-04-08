using Data;
using Data.Models;
using Moq;
using NUnit.Framework;
using Services;
using System;
using System.Collections.Generic;
using UnitTests.Data;

namespace UnitTests.Services.Tests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        Mock<RmsContext> dbContext;
        PaymentService paymentService;

        List<Property> properties;
        List<Tenant> tenants;
        List<Payment> payments;

        [SetUp]
        public void SetUp()
        {
            properties = new List<Property>
            {
                new Property {
                    Id = 1,
                    Address = "Drujba, 6",
                    Area = 100,
                    Rooms = 7,
                    Beds = 3,
                    Rent = 150
                }
            };

            tenants = new List<Tenant>()
            {
                new Tenant
                {
                    Id = 1,
                    FirstName = "Dan",
                    LastName = "Ivanov",
                    Email = "dan@abv.bg",
                    PhoneNumber = "0879542734",
                    DateOfMovingIn = DateTime.Now,
                    IsKickedOut = false,
                    RentedProperty = properties[0]

                }
            };

            payments = new List<Payment>() 
            {
                new Payment
                {
                    Id = 1,
                    Amount = 150,
                    Date = DateTime.Now,
                    Tenant = tenants[0]
                }
            };

            dbContext = new Mock<RmsContext>();

            dbContext.Setup(p => p.Tenants)
                .Returns(DbContextMock.GetQueryableMockDbSet(tenants));

            dbContext.Setup(p => p.Properties)
                .Returns(DbContextMock.GetQueryableMockDbSet(properties));

            dbContext.Setup(p => p.Payments)
                .Returns(DbContextMock.GetQueryableMockDbSet(payments));

            paymentService = new PaymentService(dbContext.Object);
        }

        [Test]
        public void GetAll_Not_Null()
        {
            IEnumerable<Payment> payments = paymentService.GetAll();

            Assert.That(payments, Is.Not.Null);
        }

        [Test]
        public void Get_Not_Null()
        {
            int paymentId = 1;
            Payment payment = paymentService.Get(paymentId);

            Assert.That(payment, Is.Not.Null);
        }

        [Test]
        public void GetAllFromTenant_Not_Null()
        {
            int tenantId = 1;
            IEnumerable<Payment> payments = paymentService.GetAllFromTenant(tenantId);

            Assert.That(payments, Is.Not.Null);
        }

        [Test]
        public void GetAmountFromMonth_Not_Null()
        {
            int month = DateTime.Now.Month;
            double amount = paymentService.GetAmountFromMonth(month);

            Assert.That(amount, Is.Not.Null);
        }

        [Test]
        public void GetPaymentSum_Not_Null()
        {
            int tenantId = 1;
            double sum = paymentService.GetPaymentSum(tenantId);

            Assert.That(sum, Is.Not.Null);
        }

        [Test]
        public void GetAllFromMonth_Not_Null()
        {
            int month = DateTime.Now.Month;
            IEnumerable<Payment> payments = paymentService.GetAllFromMonth(month);

            Assert.That(payments, Is.Not.Null);
        }

        [Test]
        public void GetAmountFromYear_Not_Null()
        {
            int year = DateTime.Now.Year;
            double sum = paymentService.GetAmountFromYear(year);

            Assert.That(sum, Is.Not.Null);
        }

        [Test]
        public void GetAmountFromAllTime_Not_Null()
        {
            double sum = paymentService.GetAmountFromAllTime();

            Assert.That(sum, Is.Not.Null);
        }
    }
}
