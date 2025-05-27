using System;
using DummyDataGenerator.Services; // 👈 import your service

namespace DummyDataGenerator
{
    public class App
    {
        public static void Main(string[] args)
        {
            var generator = new App();
            generator.Start();
        }

        public void Start()
        {
            var sqlService = new SqlDataService();

            while (true)
            {
                Console.WriteLine("\nWelcome!");
                Console.WriteLine("Type 'home' to restart.");
                Console.WriteLine("Type 'exit' to quit.");
                Console.WriteLine("Please select a process:");
                Console.WriteLine("1. SQL Data");

                Console.Write("> ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "exit") return;
                else if (input == "home") continue;
                else if (input == "1" || input == "sql data")
                {
                    sqlService.Run();
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
        }
    }
}