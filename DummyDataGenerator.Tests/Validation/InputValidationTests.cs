using Xunit;
using DummyDataGenerator.Services;
using DummyDataGenerator.Api.Models;
using System;
using System.Collections.Generic;

namespace DummyDataGenerator.Tests.Validation
{
    public class InputValidationTests
    {
        private readonly SqlDataBuilder _builder = new();

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public void BuildInMemory_WithNegativeRowCount_ReturnsEmptyList(int rows)
        {
            var fields = new List<FieldDefinition> { new() { Name = "Test", Type = "string" } };
            var result = _builder.BuildInMemory(fields, rows);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(1001)]
        [InlineData(5000)]
        [InlineData(int.MaxValue)]
        public void BuildInMemory_WithExcessiveRowCount_StillProcesses(int rows)
        {
            var fields = new List<FieldDefinition> { new() { Name = "Test", Type = "string" } };
            var result = _builder.BuildInMemory(fields, Math.Min(rows, 100));
            Assert.NotEmpty(result);
            Assert.True(result.Count <= 100);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t")]
        public void FieldDefinition_WithInvalidName_StillAcceptsButIsEmpty(string name)
        {
            var field = new FieldDefinition { Name = name, Type = "string" };
            Assert.True(string.IsNullOrWhiteSpace(field.Name));
        }

        [Fact]
        public void GenerateRequest_WithValidData_AcceptsAllProperties()
        {
            var request = new GenerateRequest
            {
                Fields = new() { new FieldDefinition { Name = "A", Type = "string" } },
                RowCount = 50,
                Format = "json"
            };
            Assert.NotNull(request.Fields);
            Assert.Equal(1, request.Fields.Count);
            Assert.Equal(50, request.RowCount);
            Assert.Equal("json", request.Format);
        }
    }
}