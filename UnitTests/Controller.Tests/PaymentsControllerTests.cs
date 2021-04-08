using Data;
using Moq;
using NUnit.Framework;
using Services;
using Rent_Management_System.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Data.Models;
using UnitTests.Data;
using System;


namespace UnitTests.Controller.Tests
{
    [TestFixture]
    public class PaymentsControllerTests
    {
        Mock<RmsContext> dbContext;
        PaymentService paymentService;
        TenantService tenantService;
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

            payments = new List<Payment>() 
            {
                new Payment
                {
                    Id = 1,
                    Amount = 100,
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
            propertyService = new PropertyService(dbContext.Object);
            tenantService = new TenantService(dbContext.Object, propertyService, paymentService);
        }

        [Test]
        public void All_Not_Null()
        {
            PaymentsController controller = new PaymentsController(paymentService, tenantService);

            ViewResult result = controller.All() as ViewResult;

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Tenant_Not_Null()
        {
            int tenantId = 1;
            PaymentsController controller = new PaymentsController(paymentService, tenantService);

            ViewResult result = controller.Tenant(tenantId) as ViewResult;

            Assert.That(result, Is.Not.Null);
        }
    }
}
