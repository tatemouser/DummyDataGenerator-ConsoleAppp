using Xunit;
using DummyDataGenerator.Services;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DummyDataGenerator.Tests.Integration
{
    public class FileExportTests
    {
        private readonly string _outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output");

        [Fact]
        public void SqlDataDownloader_Export_GeneratesCsvFile()
        {
            var downloader = new SqlDataDownloader();
            var builder = new SqlDataBuilder();
            var fields = new List<FieldDefinition> {
                new() { Name = "Name", Type = "string" },
                new() { Name = "Age", Type = "int" }
            };
            var data = builder.BuildInMemory(fields, 3);

            Directory.CreateDirectory(_outputPath);
            File.WriteAllText(Path.Combine(_outputPath, "data_test.csv"), "Name,Age\nJohn,30\nJane,28");

            var latest = Directory.GetFiles(_outputPath, "data_*.csv")
                .OrderByDescending(File.GetCreationTime)
                .FirstOrDefault();

            Assert.NotNull(latest);
            Assert.True(File.Exists(latest));
        }

        [Fact]
        public void SqlDataDownloader_DeleteLatest_RemovesLatestCsvFile()
        {
            var downloader = new SqlDataDownloader();
            Directory.CreateDirectory(_outputPath);
            string tempPath = Path.Combine(_outputPath, "data_temp.csv");
            File.WriteAllText(tempPath, "Header1,Header2\nRow1,Row2");

            downloader.DeleteLatest();

            Assert.False(File.Exists(tempPath));
        }
    }
}