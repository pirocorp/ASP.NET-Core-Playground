namespace ExchangeRates.Web.Tests
{
    using System;

    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="CurrencyConverter"/> (See section 22.1)
    /// </summary>
    public class CurrencyConverterTest
    {
        [Fact]
        public void ConvertToGbp_ConvertsCorrectly()
        {
            // Arrange - Define all the parameters and create an instance of the system (class) under test (SUT)
            #region Arrange

            // System Under Test (SUT)
            var converter = new CurrencyConverter();

            // Test parameters
            const decimal value = 3M;
            const decimal exchangeRate = 1.5m;
            const int decimalPlaces = 4;

            #endregion

            // Act - Execute the method being tested and capture the result
            #region Act

            // Actual value returned after execution
            var actual = converter.ConvertToGbp(value, exchangeRate, decimalPlaces);

            #endregion

            // Assert - Verify that the result of the Act stage had the expected value
            #region Assert

            // Expected value
            const decimal expected = 2m;

            //Verifies that the expected and actual values match. If they don't throws exception (test will fail)
            Assert.Equal(expected, actual);

            #endregion
        }

        [Theory]
        [InlineData(0, 3, 0)]
        [InlineData(3, 1.5, 2)]
        [InlineData(3.75, 2.5, 1.5)]
        public void Converts(decimal value, decimal rate, decimal expected)
        {
            var converter = new CurrencyConverter();
            const int dps = 4;

            var actual = converter.ConvertToGbp(value, rate, dps);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0), InlineData(-1), InlineData(-10000)]
        public void ThrowsExceptionIfRateIsLessThanZero(decimal rate)
        {
            var converter = new CurrencyConverter();
            const decimal value = 1;
            const int dp = 2;

            var ex = Assert.Throws<ArgumentException>(() => converter.ConvertToGbp(value, rate, dp));
        }

        [Fact]
        public void ThrowsExceptionIfDecimalPlacesIsLessThanZero()
        {
            var converter = new CurrencyConverter();
            const decimal value = 1;
            const decimal exchangeRate = 1;
            const int decimalPlaces = -2;

            Assert.Throws<ArgumentException>(() => converter.ConvertToGbp(value, exchangeRate, decimalPlaces));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.5)]
        [InlineData(-5.1)]
        [InlineData(-10.2)]
        public void ThrowsExceptionIfRateIsZeroOrLess(decimal exchangeRate)
        {
            var converter = new CurrencyConverter();
            const decimal value = 1;
            const int decimalPlaces = 2;

            Assert.Throws<ArgumentException>(() => converter.ConvertToGbp(value, exchangeRate, decimalPlaces));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(-10)]
        public void ThrowsExceptionIfDecimalPlacesIsZeroOrLess(int decimalPlaces)
        {
            var converter = new CurrencyConverter();
            const decimal value = 1;
            const decimal exchangeRate = 1;

            Assert.Throws<ArgumentException>(() => converter.ConvertToGbp(value, exchangeRate, decimalPlaces));
        }
    }
}
