using Xunit;
using DummyDataGenerator.Services;
using System;
using System.IO;
using System.Linq;

namespace DummyDataGenerator.Tests.Integration
{
    public class FilePreviewTests
    {
        private readonly string _outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output");

        [Fact]
        public void SqlDataPreviewer_Preview_ReadsFirstFiveLinesWithoutError()
        {
            // Arrange
            Directory.CreateDirectory(_outputPath);
            var testFile = Path.Combine(_outputPath, "data_preview.csv");
            File.WriteAllLines(testFile, new[] {
                "Header1,Header2",
                "Row1,Value1",
                "Row2,Value2",
                "Row3,Value3",
                "Row4,Value4",
                "Row5,Value5",
                "Row6,Value6"
            });

            var previewer = new SqlDataPreviewer();

            // Act & Assert: Just make sure it doesn't throw
            var exception = Record.Exception(() => previewer.Preview());
            Assert.Null(exception);
        }

        [Fact]
        public void SqlDataPreviewer_Preview_NoOutputFolder_DoesNotThrow()
        {
            // Arrange
            if (Directory.Exists(_outputPath))
                Directory.Delete(_outputPath, true);

            var previewer = new SqlDataPreviewer();

            // Act & Assert
            var exception = Record.Exception(() => previewer.Preview());
            Assert.Null(exception);
        }
    }
}
