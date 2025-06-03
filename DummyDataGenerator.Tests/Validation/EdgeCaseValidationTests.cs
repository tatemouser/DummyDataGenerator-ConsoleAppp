using Xunit;
using DummyDataGenerator.Services;
using System.Collections.Generic;

namespace DummyDataGenerator.Tests.Validation
{
    public class EdgeCaseValidationTests
    {
        private readonly SqlDataBuilder _builder = new();

        [Fact]
        public void GetValueForType_WithSpecialChars_ReturnsNA()
        {
            var specialTypes = new[] { "field@x", "field x", "field-x" };
            foreach (var type in specialTypes)
            {
                var result = _builder.GetValueForType(type, 0);
                Assert.Equal("N/A", result);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        public void BuildInMemory_WithBoundaryCounts_GeneratesExpectedRows(int count)
        {
            var fields = new List<FieldDefinition> { new() { Name = "Test", Type = "string" } };
            var result = _builder.BuildInMemory(fields, count);
            Assert.Equal(count, result.Count);
        }
    }
}
