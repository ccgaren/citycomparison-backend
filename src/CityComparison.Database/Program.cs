using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CityComparison.Database
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static int Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true)
               .AddEnvironmentVariables();

            Configuration = builder.Build();

            var result = Deploy("Blog");

            return (result ? 0 : 1);
        }

        private static bool Deploy(string dbName)
        {
            Console.WriteLine("Beginning for database updating '{0}'...", dbName);

            var connectionString = Configuration.GetConnectionString(dbName);
            Console.WriteLine("Connection: '{0}'.", connectionString);

            var upgrader =
               DeployChanges.To
                   .SqlDatabase(connectionString)
                   .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly, x => x.StartsWith($"Blog.Database.{dbName}."))
                   .WithTransaction()
                   .WithExecutionTimeout(new TimeSpan(0, 1, 0))
                   .LogToConsole()
                   .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                WriteError(result.Error);
                return false;
            }

            WriteSuccess(dbName);
            return true;
        }

        private static void WriteSuccess(string dbName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully updated database '{0}'.", dbName);
            Console.ResetColor();
        }

        private static void WriteError(Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exception);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
