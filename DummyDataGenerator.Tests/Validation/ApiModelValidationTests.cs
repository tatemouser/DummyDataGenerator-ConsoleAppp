using Xunit;
using DummyDataGenerator.Api.Models;
using System.Collections.Generic;

namespace DummyDataGenerator.Tests.Validation
{
    public class ApiModelValidationTests
    {
        [Fact]
        public void GenerateRequest_HasExpectedDefaults()
        {
            var req = new GenerateRequest();
            Assert.NotNull(req.Fields);
            Assert.Empty(req.Fields);
            Assert.Equal("json", req.Format);
        }

        [Theory]
        [InlineData("json")]
        [InlineData("csv")]
        public void GenerateRequest_Format_IsStored(string format)
        {
            var req = new GenerateRequest { Format = format };
            Assert.Equal(format, req.Format);
        }

        [Fact]
        public void GenerateResponse_CanStoreCsvOrJson()
        {
            var res = new GenerateResponse
            {
                CsvContent = "Header1,Header2",
                JsonData = new List<Dictionary<string, string>>()
            };
            Assert.NotNull(res.CsvContent);
            Assert.NotNull(res.JsonData);
        }
    }
}