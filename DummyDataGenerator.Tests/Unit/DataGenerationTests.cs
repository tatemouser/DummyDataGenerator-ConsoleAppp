using Xunit;
using DummyDataGenerator.Services;
using System;

namespace DummyDataGenerator.Tests.Unit
{
    /// <summary>
    /// Tests for verifying the GetValueForType method, which returns fake data
    /// for common types like name, email, phone, and so on.
    /// These are core to your data generation logic.
    /// </summary>
    public class DataGenerationTests
    {
        private readonly SqlDataBuilder _builder = new();

        [Fact]
        public void GetValueForType_Name_ReturnsValidFormat()
        {
            // Act
            string result = _builder.GetValueForType("name", 0);

            // Assert
            Assert.NotNull(result);                          // Should never be null
            Assert.StartsWith("User", result);               // Should start with "User"
            Assert.True(result.Length > 4);                  // Should have digits or suffix after
        }

        [Fact]
        public void GetValueForType_Email_ReturnsValidEmailFormat()
        {
            // Act
            string result = _builder.GetValueForType("email", 0);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("@example.com", result);         // Should use the mock domain
        }

        [Fact]
        public void GetValueForType_Phone_ReturnsValidPhoneFormat()
        {
            // Act
            string result = _builder.GetValueForType("phone", 0);

            // Assert
            Assert.Matches(@"\d{3}-\d{3}-\d{4}", result);    // Matches XXX-XXX-XXXX format
        }

        [Fact]
        public void GetValueForType_Date_ReturnsValidDate()
        {
            // Act
            string result = _builder.GetValueForType("date", 0);

            // Assert
            Assert.True(DateTime.TryParse(result, out _));   // Must be parsable as a date
        }

        [Fact]
        public void GetValueForType_Id_IsSequential()
        {
            // Act + Assert
            Assert.Equal("1", _builder.GetValueForType("id", 0));   // Index 0 -> ID 1
            Assert.Equal("6", _builder.GetValueForType("id", 5));   // Index 5 -> ID 6
        }

        [Fact]
        public void GetValueForType_Bool_IsTrueOrFalse()
        {
            // Act
            string result = _builder.GetValueForType("bool", 0);

            // Assert
            Assert.Contains(result, new[] { "true", "false" });     // Must be one of two options
        }
    }
}
