using System;
using System.Collections.Generic;
using System.IO;

namespace DummyDataGenerator.Services
{
    public class SqlDataBuilder
    {
        private readonly Random random = new Random();

        // Known templates for generating realistic data
        private readonly HashSet<string> KnownTemplates = new HashSet<string>
        {
            "name", "email", "phone", "date", "uuid", "age", "bool", "zipcode", "price", "id"
        };

        public void Build(List<FieldDefinition> fields, int rowCount)
        {
            Console.WriteLine("\nGenerating CSV file of data...");

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "output");
            Directory.CreateDirectory(folderPath); // Ensure /output/ exists

            string fileName = $"data_{DateTime.Now:yyyy-MM-dd_HHmmss}.csv";
            string fullPath = Path.Combine(folderPath, fileName);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                // Write CSV header
                var headers = string.Join(",", fields.ConvertAll(f => f.Name));
                writer.WriteLine(headers);

                // Write each row
                for (int i = 0; i < rowCount; i++)
                {
                    List<string> row = new List<string>();

                    foreach (var field in fields)
                    {
                        string value;

                        if (KnownTemplates.Contains(field.Name.ToLower()))
                        {
                            value = GetValueForType(field.Name.ToLower(), i); // Use realistic data
                        }
                        else
                        {
                            value = random.Next(1000, 9999).ToString(); // Custom field = 4-digit number
                        }

                        row.Add(value);
                    }

                    writer.WriteLine(string.Join(",", row));
                }
            }

            Console.WriteLine($"\n✅ Data exported to: {fullPath}");
        }

        private string GetValueForType(string templateName, int rowIndex)
        {
            switch (templateName)
            {
                case "name": return getName();
                case "email": return getEmail();
                case "phone": return getPhone();
                case "date": return getDate();
                case "uuid": return getUuid();
                case "age": return getAge();
                case "bool": return getBool();
                case "zipcode": return getZipcode();
                case "price": return getPrice();
                case "id": return (rowIndex + 1).ToString();
                default: return "N/A";
            }
        }

        // Generator methods
        private string getName() => $"User{random.Next(1000, 9999)}";

        private string getEmail() => $"user{random.Next(1000, 9999)}@example.com";

        private string getPhone()
        {
            int area = random.Next(100, 999);
            int mid = random.Next(100, 999);
            int end = random.Next(1000, 9999);
            return $"{area}-{mid}-{end}";
        }

        private string getDate()
        {
            var date = DateTime.Today.AddDays(-random.Next(0, 1000));
            return date.ToString("yyyy-MM-dd");
        }

        private string getUuid() => Guid.NewGuid().ToString();

        private string getAge() => random.Next(18, 80).ToString();

        private string getBool() => random.Next(2) == 0 ? "true" : "false";

        private string getZipcode() => random.Next(10000, 99999).ToString();

        private string getPrice() => Math.Round(random.NextDouble() * 100, 2).ToString("F2");
    }
}
