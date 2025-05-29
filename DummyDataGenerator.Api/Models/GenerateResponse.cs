// DummyDataGenerator.Api/Models/GenerateResponse.cs
namespace DummyDataGenerator.Api.Models
{
    public class GenerateResponse
    {
        public string? CsvContent { get; set; }
        public List<Dictionary<string, string>>? JsonData { get; set; }
    }
}
