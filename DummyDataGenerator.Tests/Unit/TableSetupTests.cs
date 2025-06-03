using Xunit;
using DummyDataGenerator.Services;
using System.Collections.Generic;
using System.Linq;

namespace DummyDataGenerator.Tests.Unit
{
    /// <summary>
    /// Tests that FieldDefinition and table-building logic behave correctly.
    /// </summary>
    public class TableSetupTests
    {
        [Fact]
        public void FieldDefinition_DefaultConstructor_SetsDefaultValues()
        {
            // Act
            var field = new FieldDefinition();

            // Assert
            Assert.Equal(string.Empty, field.Name);           // Default name
            Assert.Equal("string", field.Type);               // Default type
        }

        [Fact]
        public void FieldDefinition_CustomValues_StoresCorrectly()
        {
            // Act
            var field = new FieldDefinition
            {
                Name = "TestField",
                Type = "int"
            };

            // Assert
            Assert.Equal("TestField", field.Name);
            Assert.Equal("int", field.Type);
        }

        [Fact]
        public void BuildInMemory_WithCustomFields_GeneratesCorrectStructure()
        {
            // Arrange: define custom fields
            var builder = new SqlDataBuilder();
            var fields = new List<FieldDefinition>
            {
                new() { Name = "Name", Type = "string" },
                new() { Name = "Age", Type = "int" },
                new() { Name = "Email", Type = "string" }
            };

            // Act: build 2 rows in memory
            var result = builder.BuildInMemory(fields, 2);

            // Assert: Check structure and field presence
            Assert.Equal(2, result.Count);
            Assert.All(result, row => Assert.True(row.ContainsKey("Name")));
            Assert.All(result, row => Assert.True(row.ContainsKey("Age")));
            Assert.All(result, row => Assert.True(row.ContainsKey("Email")));
        }
    }
}
