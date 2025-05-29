// DummyDataGenerator.Api/Models/GenerateRequest.cs
using DummyDataGenerator.Services; 

namespace DummyDataGenerator.Api.Models
{
    public class GenerateRequest
    {
        public List<FieldDefinition> Fields { get; set; } = new();
        public int RowCount { get; set; }
        public string Format { get; set; } = "json"; // "json" or "csv"
    }
}
