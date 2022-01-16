using NUnit.Framework;
using Kerstdiner.Models;

namespace Kerstdiner.Tests
{
    [TestFixture]
    [SetCulture("nl-BE")]
    public class ModelTests
    {
        [Test]
        public void DinerReservatie_ToString_ReturnsValidOutput()
        {
            var subject = new DinerReservatie("Smith", 1, "Steak", 69);

            Assert.AreEqual("Diner\tSmith\t69,00 €", subject.ToString());
        }

        [Test]
        public void DinerReservatie_Throws_ValueRequiredException_When_NameEmpty()
        {
            Assert.Catch<ValueRequiredException>(delegate
            {
                var subject = new DinerReservatie("", 2, "Steak", 69);
            });
        }

        [Test]
        public void DinerReservatie_Throws_ValueZeroOrNegativeException_When_AantalLt1()
        {
            Assert.Catch<ValueZeroOrNegativeException>(delegate
            {
                var subject = new DinerReservatie("Winchester", 0, "Steak", 69);
            });
        }

        [Test]
        public void Gerecht_ToString_ReturnsValidOutput()
        {
            var subject = new Gerecht("Hoofd", "Tongrolletjes", 25.2);

            Assert.AreEqual("Tongrolletjes - 25,20 €", subject.ToString());
        }
    }
}