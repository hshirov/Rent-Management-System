using Data;
using Moq;
using NUnit.Framework;
using Services;
using Rent_Management_System.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Data.Models;
using UnitTests.Data;

namespace UnitTests.Controller.Tests
{
    [TestFixture]
    public class HomeControllerTests
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
            tenants = new List<Tenant>();
            properties = new List<Property>();
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
        public void Index_Not_Null()
        {
            HomeController controller = new HomeController(propertyService, tenantService, paymentService);

            ViewResult result = controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
        }
    }
}
