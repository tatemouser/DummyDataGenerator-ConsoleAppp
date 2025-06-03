using System;
using System.IO;
using System.Linq;

namespace DummyDataGenerator.Services
{
    public class SqlDataPreviewer
    {
        public void Preview()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "output");

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("❌ No output folder found.");
                return;
            }

            var files = Directory.GetFiles(folderPath, "data_*.csv");

            if (files.Length == 0)
            {
                Console.WriteLine("❌ No generated data file found to preview.");
                return;
            }

            // Get the latest file by creation time
            var latestFile = files.OrderByDescending(File.GetCreationTime).First();

            Console.WriteLine($"\n📄 Previewing: {Path.GetFileName(latestFile)}");

            int maxLines = 5;
            int count = 0;

            using (var reader = new StreamReader(latestFile))
            {
                while (!reader.EndOfStream && count < maxLines)
                {
                    var line = reader.ReadLine();
                    Console.WriteLine(line);
                    count++;
                }
            }

            Console.WriteLine("\n🔍 End of preview (first 5 rows).");
        }
    }
}
