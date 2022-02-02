using BoatTrackerDomain.DataTransferObjects;
using BoatTrackerDomain.Models;
using BoatTrackerDomain.Repository;
using BoatTrackerDomain.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Model
{
    [TestFixture]
    public class BoatDataManagerFixture
    {
        [Test]
        public void CanCreateInstance()
        {
            var mockSet = new Mock<DbSet<Boat>>();
            var mockContext = new Mock<BoatTrackerContext>();
            mockContext.Setup(m => m.Boats).Returns(mockSet.Object);

            var sut = new BoatDataManagers(mockContext.Object);

            sut.Should().NotBeNull();
        }

        [Test]
        public void ShouldThrowExceptionsIfContextIsEmpty()
        {

            Action sut = () => new BoatDataManagers(null);

            sut.Should()
                .Throw<ArgumentNullException>();
        }
    }
}
