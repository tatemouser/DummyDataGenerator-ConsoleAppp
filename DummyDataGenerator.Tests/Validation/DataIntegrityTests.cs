using Xunit;
using DummyDataGenerator.Services;
using System.Collections.Generic;
using System.Linq;

namespace DummyDataGenerator.Tests.Validation
{
    public class DataIntegrityTests
    {
        private readonly SqlDataBuilder _builder = new();

        [Fact]
        public void BuildInMemory_GeneratedData_HasConsistentFieldCount()
        {
            var fields = new List<FieldDefinition> {
                new() { Name = "Name", Type = "string" },
                new() { Name = "Email", Type = "string" }
            };
            var result = _builder.BuildInMemory(fields, 10);
            Assert.All(result, row => Assert.Equal(2, row.Count));
        }

        [Fact]
        public void GetValueForType_Id_IsSequential()
        {
            var ids = Enumerable.Range(0, 5)
                .Select(i => _builder.GetValueForType("id", i)).ToList();
            Assert.Equal(new List<string> { "1", "2", "3", "4", "5" }, ids);
        }

        [Fact]
        public void GetValueForType_Email_HasValidFormat()
        {
            var emails = Enumerable.Range(0, 5)
                .Select(i => _builder.GetValueForType("email", i));
            Assert.All(emails, e => Assert.Contains("@example.com", e));
        }

        [Fact]
        public void BuildInMemory_MultipleRuns_ProduceDifferentData()
        {
            var fields = new List<FieldDefinition> { new() { Name = "name", Type = "string" } };
            var run1 = _builder.BuildInMemory(fields, 5).Select(r => r["name"]).ToList();
            var run2 = _builder.BuildInMemory(fields, 5).Select(r => r["name"]).ToList();
            Assert.NotEqual(run1, run2);
        }
    }
}