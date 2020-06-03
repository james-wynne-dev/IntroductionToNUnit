using Loans.Domain.Applications;
using NUnit.Framework;
using System;

namespace Loans.Tests
{
    // Can use Ignore tests at class level as well
    [TestFixture] // This TestFixture is not necessary
    public class LoanTermShould
    {
        
        [Test]
        [Ignore("Message goes here!!!")] // prevent test from running
        public void ReturnTermInMonths()
        {
            // sut stands for system under test
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12), "Custom Error Message");
        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        // Testing with floating point numbers
        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;

            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowZeroYears()
        {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());

            // checking property
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Property("Message")
                .EqualTo("Please specify a value greater than 0.\r\nParameter name: years")); // specifiying property as string, not type safe

            // checking property, type safe
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Message
                .EqualTo("Please specify a value greater than 0.\r\nParameter name: years"));

            // correct ex and para name but don't care about the message
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Property("ParamName")
                .EqualTo("years"));

            // correct ex and para name but don't care about the message, type safe
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Matches<ArgumentOutOfRangeException>(
                    ex => ex.ParamName == "years"));
        }
    }
}
