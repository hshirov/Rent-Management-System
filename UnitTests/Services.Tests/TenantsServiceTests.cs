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
    public class TenantsServiceTests
    {
        Mock<RmsContext> dbContext;

        TenantService tenantService;
        PaymentService paymentService;
        PropertyService propertyService;

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

            payments = new List<Payment>();

            dbContext = new Mock<RmsContext>();

            dbContext.Setup(p => p.Tenants)
                .Returns(DbContextMock.GetQueryableMockDbSet(tenants));

            dbContext.Setup(p => p.Properties)
                .Returns(DbContextMock.GetQueryableMockDbSet(properties));

            dbContext.Setup(p => p.Payments)
                .Returns(DbContextMock.GetQueryableMockDbSet(payments));

            paymentService = new PaymentService(dbContext.Object);
            propertyService = new PropertyService(dbContext.Object);
            tenantService = new TenantService(dbContext.Object, propertyService, paymentService);
        }

        [Test]
        public void GetAll_Not_Null()
        {
            IEnumerable<Tenant> tenants = tenantService.GetAll();

            Assert.That(tenants, Is.Not.Null);
        }

        [Test]
        public void Get_Not_Null()
        {
            int tenantId = 1;
            Tenant tenant = tenantService.Get(tenantId);

            Assert.That(tenant, Is.Not.Null);
        }

        [Test]
        public void GetAllFromProperty_Not_Null()
        {
            int propertyId = 1;
            IEnumerable<Tenant> tenants = tenantService.GetAllFromProperty(propertyId);

            Assert.That(tenants, Is.Not.Null);
        }

        [Test]
        public void GetMoneyOwed_Not_Null()
        {
            int tenantId = 1;
            double debt = tenantService.GetMoneyOwed(tenantId);

            Assert.That(debt, Is.Not.Null);
        }

        [Test]
        public void GetNumberOfTenants_Not_Null()
        {
            int count = tenantService.GetNumberOfTenants();

            Assert.That(count, Is.Not.Null);
        }

        [Test]
        public void IsEmailTaken_Existing_Email_True()
        {
            string email = tenants[0].Email;
            bool result = tenantService.IsEmailTaken(email);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsEmailTaken_Not_Existing_Email_False()
        {
            string email = "thisemailhasntbeenusedbefore@gmail.com";
            bool result = tenantService.IsEmailTaken(email);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetNumberOfTenantsInProperty_NotNull()
        {
            int propertyId = 1;
            int count = tenantService.GetNumberOfTenantsInProperty(propertyId);

            Assert.That(count, Is.Not.Null);
        }

        [Test]
        public void HasPayments_No_Payments_False()
        {
            int tenantId = 1;
            bool result = tenantService.HasPayments(tenantId);

            Assert.That(result, Is.False);
        }
    }
}
