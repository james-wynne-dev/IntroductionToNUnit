using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // gets ran before all the tests
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // gets ran after all tests complete
        }


        // This setup method is being ran before each test method runs
        [SetUp]
        public void Setup()
        {
            products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }

        [TearDown]
        public void TearDown()
        {
            // Runs after each test executes
            // sut.Dispose(); // if implements IDisposable
        }

        [Test]
        [Category("Product Comparison")]
        public void ReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }


        // Tests that all the items in a collection are unique
        [Test]
        public void NotReturnDuplicateComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        // Tests that the collection contains a specific item
        // Need to know the result
        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnowExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            // Don't care about the expected monthly replayment, only that the product is there
            // Note that the properties are specified as strings, this is vunerable to code changes
            Assert.That(comparisons, Has.Exactly(1)
                .Property("ProductName").EqualTo("a")
                .And
                .Property("InterestRate").EqualTo(1)
                .And
                .Property("MonthlyRepayment").GreaterThan(0));

            // Same test but using type and lambda
            Assert.That(comparisons, Has.Exactly(1)
                .Matches<MonthlyRepaymentComparison>(
                    item => item.ProductName == "a" &&
                            item.InterestRate == 1 &&
                            item.MonthlyRepayment > 0));

            Assert.That(comparisons, Has.Exactly(1)
                .Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a", 1)));
        }
    }
}
