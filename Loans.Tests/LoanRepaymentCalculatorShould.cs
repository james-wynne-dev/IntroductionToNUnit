using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanRepaymentCalculatorShould
    {
        // Test case passes in an array of values to the method
        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        [TestCase(500_000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyRepayment(decimal principal, decimal interestRate,
            int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                     new LoanAmount("USD", principal),
                                     interestRate,
                                     new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyRepayment_SimplifiedTestCase(decimal principal, decimal interestRate,
            int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(
                                     new LoanAmount("USD", principal),
                                     interestRate,
                                     new LoanTerm(termInYears));
        }

        // Getting data from external class
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralised(decimal principal, decimal interestRate,
            int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                     new LoanAmount("USD", principal),
                                     interestRate,
                                     new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(decimal principal, decimal interestRate,
            int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(
                                     new LoanAmount("USD", principal),
                                     interestRate,
                                     new LoanTerm(termInYears));
        }

        // Getting data from csv file
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_Csv(decimal principal, decimal interestRate,
            int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                     new LoanAmount("USD", principal),
                                     interestRate,
                                     new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        // This does not assert a result but just provides combinations of inputs to see if an exception is thrown
        [Test]
        public void CalculateCorrectMonthlyRepayment_Combinatorial(
            [Values(100_000, 200_000, 500_000)]decimal principal,
            [Values(6.5, 10, 20)]decimal interestRate,
            [Values(10, 20, 30)]int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        // Same as providing three test cases
        [Test]
        [Sequential]
        public void CalculateCorrectMonthlyRepayment_Sequential(
                    [Values(200_000, 200_000, 500_000)]decimal principal,
                    [Values(6.5, 10, 10)]decimal interestRate,
                    [Values(30, 30, 30)]int termInYears,
                    [Values(1264.14, 1755.14, 4387.86)]decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        // Can use ranges to provide inputs
        // Range: start, end, step
        [Test]
        public void CalculateCorrectMonthlyRepayment_Range(
            [Range(50_000, 1_000_000, 50_000)]decimal principal,
            [Range(0.5, 20.00, 0.5)]decimal interestRate,
            [Values(10, 20, 30)]int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }
    }
}
