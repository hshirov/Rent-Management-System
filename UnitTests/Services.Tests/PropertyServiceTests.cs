using Data;
using Data.Models;
using Moq;
using NUnit.Framework;
using Services;
using System.Collections.Generic;
using UnitTests.Data;

namespace UnitTests.Services.Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        Mock<RmsContext> dbContext;
        PropertyService propertyService;
        List<Property> properties;

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

            dbContext = new Mock<RmsContext>();

            dbContext.Setup(p => p.Properties)
                .Returns(DbContextMock.GetQueryableMockDbSet(properties));

            propertyService = new PropertyService(dbContext.Object);
        }

        [Test]
        public void GetAll_Not_Null()
        {
            IEnumerable<Property> properties = propertyService.GetAll();

            Assert.That(properties, Is.Not.Null);
        }

        [Test]
        public void Get_Not_Null()
        {
            int propertyId = 1;
            Property property = propertyService.Get(propertyId);

            Assert.That(property, Is.Not.Null);
        }

    }
}
