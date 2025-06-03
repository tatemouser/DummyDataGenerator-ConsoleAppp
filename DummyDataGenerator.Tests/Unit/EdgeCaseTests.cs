using Xunit;
using DummyDataGenerator.Services;
using System.Collections.Generic;

namespace DummyDataGenerator.Tests.Unit
{
    /// <summary>
    /// Tests for inputs that could break or confuse your logic,
    /// like zero rows, empty fields, or unknown types.
    /// </summary>
    public class EdgeCaseTests
    {
        private readonly SqlDataBuilder _builder = new();

        [Fact]
        public void BuildInMemory_WithEmptyFieldList_ReturnsEmptyRows()
        {
            var result = _builder.BuildInMemory(new List<FieldDefinition>(), 3);

            Assert.Equal(3, result.Count);               // Still returns correct row count
            Assert.All(result, row => Assert.Empty(row)); // But each row is empty
        }

        [Fact]
        public void BuildInMemory_WithZeroRows_ReturnsEmptyList()
        {
            var fields = new List<FieldDefinition>
            {
                new() { Name = "TestField", Type = "string" }
            };

            var result = _builder.BuildInMemory(fields, 0);

            Assert.Empty(result); // Should return zero rows
        }

        [Fact]
        public void GetValueForType_WithUnknownType_ReturnsNA()
        {
            string result = _builder.GetValueForType("invalidtype", 0);
            Assert.Equal("N/A", result); // Default fallback
        }

        [Fact]
        public void GetValueForType_WithNullOrEmptyType_ReturnsNA()
        {
            Assert.Equal("N/A", _builder.GetValueForType("", 0));
            Assert.Equal("N/A", _builder.GetValueForType(null, 0));
        }
    }
}
