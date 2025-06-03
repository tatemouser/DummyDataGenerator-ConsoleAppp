using Xunit;
using DummyDataGenerator.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DummyDataGenerator.Tests.Unit
{
    /// <summary>
    /// Tests that ensure data export logic produces the right number of rows and
    /// correctly interprets templates.
    /// </summary>
    public class ExportLogicTests
    {
        private readonly SqlDataBuilder _builder = new();
        private readonly string _testOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "test_output");

        public ExportLogicTests()
        {
            Directory.CreateDirectory(_testOutputPath); // Safe output directory for test scenarios
        }

        [Fact]
        public void BuildInMemory_GeneratesCorrectNumberOfRows()
        {
            var fields = new List<FieldDefinition>
            {
                new() { Name = "TestField", Type = "string" }
            };

            var result = _builder.BuildInMemory(fields, 5);

            Assert.Equal(5, result.Count);                  // Expect 5 rows
            Assert.All(result, row => Assert.Single(row));  // Each row has exactly 1 field
        }

        [Fact]
        public void BuildInMemory_WithKnownTemplates_UsesCorrectGenerators()
        {
            var fields = new List<FieldDefinition>
            {
                new() { Name = "email", Type = "string" },
                new() { Name = "customfield", Type = "string" }
            };

            var result = _builder.BuildInMemory(fields, 1);
            var row = result.First();

            Assert.Contains("@example.com", row["email"]);         // Email template check
            Assert.Matches(@"\d{4}", row["customfield"]);          // Custom fallback generator
        }

        [Fact]
        public void KnownTemplates_ContainsExpectedValues()
        {
            // Templates should be registered for common types
            Assert.Contains("name", _builder.KnownTemplates);
            Assert.Contains("email", _builder.KnownTemplates);
            Assert.Contains("phone", _builder.KnownTemplates);
            Assert.Contains("date", _builder.KnownTemplates);
            Assert.Equal(10, _builder.KnownTemplates.Count);       // Expected total
        }
    }
}
