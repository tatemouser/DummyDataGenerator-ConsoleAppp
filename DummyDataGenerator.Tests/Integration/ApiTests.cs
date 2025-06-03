using DummyDataGenerator.Api; 
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DummyDataGenerator.Tests.Integration
{
    public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Generate_ReturnsJsonAnd200()
        {
            // Arrange
            var json = """
            {
                "fields": [
                    { "name": "Name", "type": "string" },
                    { "name": "Age", "type": "int" }
                ],
                "rowCount": 5,
                "format": "json"
            }
            """;

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/generate", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status code 200-299
            Assert.Contains("Name", responseBody);
            Assert.Contains("Age", responseBody);
        }
    }
}
