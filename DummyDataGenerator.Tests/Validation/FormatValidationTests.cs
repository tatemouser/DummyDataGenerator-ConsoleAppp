using Xunit;
using DummyDataGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DummyDataGenerator.Tests.Validation
{
    public class FormatValidationTests
    {
        private readonly SqlDataBuilder _builder = new();

        [Fact]
        public void GetValueForType_Phone_IsCorrectFormat()
        {
            var phones = Enumerable.Range(0, 10).Select(i => _builder.GetValueForType("phone", i));
            Assert.All(phones, phone => Assert.Matches(@"^\d{3}-\d{3}-\d{4}$", phone));
        }

        [Fact]
        public void GetValueForType_Uuid_IsValidGuid()
        {
            var uuids = Enumerable.Range(0, 5).Select(i => _builder.GetValueForType("uuid", i));
            Assert.All(uuids, uuid => Assert.True(Guid.TryParse(uuid, out _)));
        }

        [Fact]
        public void GetValueForType_Bool_IsTrueOrFalse()
        {
            var values = Enumerable.Range(0, 50).Select(i => _builder.GetValueForType("bool", i));
            Assert.Contains("true", values);
            Assert.Contains("false", values);
        }
    }
}
