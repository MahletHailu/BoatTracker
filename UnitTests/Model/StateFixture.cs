using BoatTrackerDomain.Models;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace UnitTests.Model
{
    [TestFixture]
    public class StateFixture
    {
        [Test]
        public void CanCreateInstance()
        {
            var sut = new State();

            sut.Should().NotBeNull();
        }

        [Test]
        public void SettingIDChangesID()
        {
            var id = Convert.ToByte(1);

            var sut = new State
            {
                Id = id
            };

            sut.Id.Should().Be(id);
        }

        [Test]
        public void SettingDescriptionChangesDescription()
        {
            var description = "Test";

            var sut = new State
            {
                Description = description
            };

            sut.Description.Should().Be(description);
        }

        [Test]
        public void ConstructorShouldDefaultToIdZero()
        {
            var sut = new Boat();

            sut.State.Should().Be(Convert.ToByte(0));
        }

        [TestCase(10)]
        [TestCase(3)]
        public void InvalidIdValueShouldThrowException(int state)
        {
            var invalidState = Convert.ToByte(state);

            Action sut = () => new State
            {
                Id = invalidState
            };

            sut.Should()
                .Throw<ArgumentOutOfRangeException>();
        }
    }
}
