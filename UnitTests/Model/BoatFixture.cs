using BoatTrackerDomain.Models;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace UnitTests.Model
{
    [TestFixture]
    public class BoatFixture
    {
        [Test]
        public void CanCreateInstance()
        {
            var sut = new Boat();

            sut.Should().NotBeNull();
        }

        [Test]
        public void SettingHINChangesBoatHIN()
        {
            var hin = "ADR4682348";

            var sut = new Boat
            {
                HIN = hin
            };

            sut.HIN.Should().Be(hin);
        }

        [Test]
        public void SettingNameChangesName()
        {
            var name = "Test";

            var sut = new Boat
            {
                Name = name
            };

            sut.Name.Should().Be(name);
        }

        [Test]
        public void SettingStateChangesBoatState()
        {
            byte state = 1;

            var sut = new Boat
            {
                State = 1
            };

            sut.State.Should().Be(state);
        }

        [Test]
        public void ConstructorShouldDefaultToEmptyHIN()
        {
            var sut = new Boat();

            sut.HIN.Should().Be(string.Empty);
        }

        [Test]
        public void ConstructorShouldDefaultToEmptyName()
        {
            var sut = new Boat();

            sut.Name.Should().Be(string.Empty);
        }

        [Test]
        public void ConstructorShouldDefaultToStateZero()
        {
            var sut = new Boat();

            sut.State.Should().Be(0);
        }

        [TestCase(10)]
        [TestCase(5)]
        public void InvalidStateValueShouldThrowException(int state)
        {
            var invalidState = Convert.ToByte(state);

            Action sut = () => new Boat
            {
                State = invalidState
            };

            sut.Should()
                .Throw<ArgumentOutOfRangeException>();
        }
    }
}
