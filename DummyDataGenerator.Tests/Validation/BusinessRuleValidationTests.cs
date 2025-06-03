using Xunit;
using DummyDataGenerator.Services;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DummyDataGenerator.Tests.Validation
{
    public class BusinessRuleValidationTests
    {
        private readonly SqlDataBuilder _builder = new();

        [Fact]
        public void GetValueForType_Age_IsWithinRange()
        {
            var values = Enumerable.Range(0, 50).Select(i => int.Parse(_builder.GetValueForType("age", i)));
            Assert.All(values, v => Assert.InRange(v, 18, 80));
        }

        [Fact]
        public void GetValueForType_Price_HasMonetaryFormat()
        {
            var values = Enumerable.Range(0, 20).Select(i => _builder.GetValueForType("price", i));
            Assert.All(values, price =>
            {
                Assert.Matches(@"\d+\.\d{2}", price);
                Assert.True(decimal.Parse(price) >= 0);
            });
        }

        [Fact]
        public void GetValueForType_Zipcode_HasFiveDigits()
        {
            var zips = Enumerable.Range(0, 10).Select(i => _builder.GetValueForType("zipcode", i));
            Assert.All(zips, z => Assert.Matches(@"^\d{5}$", z));
        }

        [Fact]
        public void GetValueForType_Date_IsRecent()
        {
            var dates = Enumerable.Range(0, 10).Select(i => DateTime.Parse(_builder.GetValueForType("date", i)));
            var limit = DateTime.Today.AddDays(-1000);
            Assert.All(dates, d => Assert.True(d >= limit && d <= DateTime.Today));
        }
    }
}