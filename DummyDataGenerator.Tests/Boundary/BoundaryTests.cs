using System;
using System.Collections.Generic;
using Xunit;
using DummyDataGenerator.Services;

namespace DummyDataGenerator.Tests.Boundary
{
    /// <summary>
    /// Tests boundary conditions like min/max row count, field limits, data ranges, and string lengths.
    /// </summary>
    public class BoundaryTests
    {
        private readonly SqlDataBuilder _builder = new();

        // ------------------------
        // 1. ROW COUNT BOUNDARIES
        // ------------------------

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        public void BuildInMemory_RowCountBoundaries_HandlesCorrectly(int rowCount)
        {
            var fields = new List<FieldDefinition> {
                new() { Name = "TestField", Type = "string" }
            };
            var result = _builder.BuildInMemory(fields, rowCount);
            Assert.Equal(rowCount, result.Count);
        }

        // ------------------------
        // 2. FIELD COUNT BOUNDARIES
        // ------------------------

        [Fact]
        public void BuildInMemory_MinimumFields_EmptyFieldList()
        {
            var fields = new List<FieldDefinition>();
            var result = _builder.BuildInMemory(fields, 5);
            Assert.Equal(5, result.Count);
            Assert.All(result, row => Assert.Empty(row));
        }

        [Fact]
        public void BuildInMemory_MaximumFields_TenFields()
        {
            var fields = new List<FieldDefinition>();
            for (int i = 1; i <= 10; i++)
                fields.Add(new FieldDefinition { Name = $"Field{i}", Type = "string" });

            var result = _builder.BuildInMemory(fields, 3);
            Assert.Equal(3, result.Count);
            Assert.All(result, row => Assert.Equal(10, row.Count));
        }

        // ------------------------
        // 3. DATA TYPE BOUNDARIES
        // ------------------------

        [Fact]
        public void GetValueForType_Age_BoundaryValues()
        {
            var ages = new List<int>();
            for (int i = 0; i < 200; i++)
                ages.Add(int.Parse(_builder.GetValueForType("age", i)));

            Assert.Contains(18, ages);
            Assert.All(ages, age => Assert.InRange(age, 18, 80));
        }

        [Fact]
        public void GetValueForType_Price_BoundaryValues()
        {
            var prices = new List<decimal>();
            for (int i = 0; i < 100; i++)
                prices.Add(decimal.Parse(_builder.GetValueForType("price", i)));

            Assert.All(prices, price => Assert.InRange(price, 0.00m, 100.00m));
            Assert.Contains(prices, p => p <= 1.00m);
        }

        [Fact]
        public void GetValueForType_Zipcode_BoundaryValues()
        {
            var zipcodes = new List<int>();
            for (int i = 0; i < 50; i++)
                zipcodes.Add(int.Parse(_builder.GetValueForType("zipcode", i)));

            Assert.All(zipcodes, z => Assert.InRange(z, 10000, 99999));
            Assert.All(zipcodes, z => Assert.Equal(5, z.ToString().Length));
        }

        // ------------------------
        // 4. STRING LENGTH BOUNDARIES
        // ------------------------

        [Fact]
        public void GetValueForType_Name_ConsistentLength()
        {
            var names = new List<string>();
            for (int i = 0; i < 20; i++)
                names.Add(_builder.GetValueForType("name", i));

            Assert.All(names, name =>
            {
                Assert.Equal(8, name.Length);
                Assert.StartsWith("User", name);
            });
        }

        [Fact]
        public void GetValueForType_Phone_ExactLength()
        {
            var phones = new List<string>();
            for (int i = 0; i < 15; i++)
                phones.Add(_builder.GetValueForType("phone", i));

            Assert.All(phones, phone => Assert.Equal(12, phone.Length));
        }
    }
}
