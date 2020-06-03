using System;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [Category("Class Level Category")]
    public class MonthlyRepaymentComparisonShould
    {
        // Test in multiple categories
        [Test]
        [ProductComparison]
        [Category("xyz")]
        public void RespectValueEquality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        [Category("xyz")]
        public void RespectValueInequality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 42.42m, 23.22m);

            Assert.That(a, Is.Not.EqualTo(b));
        }
    }
}
