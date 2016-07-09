﻿using System;
using System.Configuration;
using ChickenSoftware.CustomerProcessor;
using ChickenSoftware.Processor.FS;

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
            //var notifier = new CDyneTextNotifier(phoneNumbers,licenseKey);
            var notifier = new TwilioTextNotifier(String.Empty, String.Empty, String.Empty, phoneNumbers);

            var processor = new CustomerProcessor(connectionString, notifier);
            Console.WriteLine("Ending...");
            Console.ReadKey();
        }
    }
}
