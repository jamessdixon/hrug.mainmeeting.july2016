using System;
using System.Configuration;
using ChickenSoftware.CustomerProcessor;

namespace ChickenSoftware.Processor.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting....");

            string connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;

            string[] phoneNumbers = new string[] { "919-555-1212", "919-867-5309" };
            Guid licenseKey = Guid.NewGuid();
            var notifier = new CDyneTextNotifier(phoneNumbers,licenseKey);

            var processor = new CustomerProcessor(connectionString, notifier);
            Console.WriteLine("Ending...");
            Console.ReadKey();
        }
    }
}
