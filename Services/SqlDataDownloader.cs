using System;
using System.IO;
using System.Linq;

namespace DummyDataGenerator.Services
{
    public class SqlDataDownloader
    {
        public void Download()
        {
            Console.WriteLine("📦 Your file is ready and has already been saved in the output folder.");
        }

        public void DeleteLatest()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "output");

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("⚠️ Output folder not found. Nothing to delete.");
                return;
            }

            var files = Directory.GetFiles(folderPath, "data_*.csv");

            if (files.Length == 0)
            {
                Console.WriteLine("⚠️ No file found to delete.");
                return;
            }

            var latestFile = files.OrderByDescending(File.GetCreationTime).First();

            try
            {
                File.Delete(latestFile);
                Console.WriteLine($"🗑️ File deleted: {Path.GetFileName(latestFile)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to delete file: {ex.Message}");
            }
        }
    }
}
