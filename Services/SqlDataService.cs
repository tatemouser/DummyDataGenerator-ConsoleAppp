using System;
using System.Collections.Generic;

namespace DummyDataGenerator.Services
{
    public class SqlDataService
    {
        // Supported data types
        private static readonly HashSet<string> SupportedTypes = new HashSet<string>
        {
            "string", "int", "date", "bool"
        };

        public void Run()
        {
            // Get number of oclumns
            Console.WriteLine("\nSQL Data Service is running...");

            int columnCount = 0;
            while (true)
            {
                Console.WriteLine("How many columns do you need? (1–10)");
                string? input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out columnCount) && columnCount >= 1 && columnCount <= 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Invalid input. Please enter a number between 1 and 10.");
                }
            }

            // Get name and type for each column
            List<FieldDefinition> fields = new List<FieldDefinition>();

            for (int i = 1; i <= columnCount; i++)
            {
                string? name;
                while (true)
                {
                    Console.Write($"\nEnter custom name for column #{i} or type a format: ");
                    Console.WriteLine("Basic formats: [name] [email] [phone] [date] [uuid] [age] [bool] [city] [zipcode] [price] [id]");
                    Console.WriteLine("Note: All custom names will generate a random 4-digit number.");
                    name = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(name))
                        break;
                    Console.WriteLine("❌ Column name cannot be empty.");
                }

                string? type;
                while (true)
                {
                    Console.WriteLine($"\nType the number for column '{name}' type:");
                    Console.WriteLine("1. string");
                    Console.WriteLine("2. int");
                    Console.WriteLine("3. date");
                    Console.WriteLine("4. bool");
                    Console.Write("> ");

                    string? input = Console.ReadLine()?.Trim().ToLower();

                    // Handle number input (e.g. 1, 2, 3, 4)
                    if (int.TryParse(input, out int typeNumber))
                    {
                        switch (typeNumber)
                        {
                            case 1: type = "string"; break;
                            case 2: type = "int"; break;
                            case 3: type = "date"; break;
                            case 4: type = "bool"; break;
                            default:
                                Console.WriteLine("❌ Invalid number. Please choose 1–4.");
                                continue;
                        }
                    }
                    // Handle direct string input (e.g. string, int)
                    else if (SupportedTypes.Contains(input))
                    {
                        type = input;
                    }
                    else
                    {
                        Console.WriteLine("❌ Unsupported type. Try again.");
                        continue;
                    }

                    break; // If type is valid, exit the loop
                }

                fields.Add(new FieldDefinition { Name = name, Type = type });
            }

            // Print structure
            Console.WriteLine("\n✅ Table structure:");
            foreach (var field in fields)
            {
                Console.WriteLine($"- {field.Name}: {field.Type}");
            }

            int rowCount = 0;

            while (true)
            {
                Console.Write("\nHow many rows would you like? [1-1000]: ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (int.TryParse(input, out int rows))
                {
                    if (rows >= 1 && rows <= 1000)
                    {
                        rowCount = rows;
                        break; 
                    }
                }
                Console.WriteLine("❌ Invalid input. Please enter a number between 1 and 1000.");
            }

            // Generate Data
            var generator = new SqlDataBuilder();
            generator.Build(fields, rowCount);

            // Preview Data
            var previewer = new SqlDataPreviewer();
            previewer.Preview();

            while (true)
            {
                Console.WriteLine("Type 'yes' to download data to output folder or 'no' to remove it:");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "yes")
                {
                    Console.WriteLine("✅ Data will be downloaded to the output folder.");
                    var downloader = new SqlDataDownloader();
                    downloader.Download();
                    break;
                }
                else if (input == "no")
                {
                    Console.WriteLine("❌ Data will not be downloaded. Removing latest file...");
                    var downloader = new SqlDataDownloader();
                    downloader.DeleteLatest();
                    return;
                }
                else
                {
                    Console.WriteLine("❌ Invalid input. Please type 'yes' or 'no'.");
                }
            }
        }
    }

    public class FieldDefinition
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
