using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenSoftware.Core;

namespace ChickenSoftware.Processor
{
    public class CustomerProcessor
    {
        String _connectionString = String.Empty;
        INotifier _notifier = null;

        public CustomerProcessor(String connectionString, INotifier notifer)
        {
            _connectionString = connectionString;
            _notifier = notifer;
        }

        public Int32 RegisterNewCustomer(Customer customer)
        {
            Int32 customerId = 0;
            if (IsCustomerValid(customer))
            {
                customerId = InsertCustomer(customer, _connectionString);
                customer.CustomerId = customerId;
                SendCustomerProcessedNotification(customer);
            }
            return customerId;
        }

        internal bool IsCustomerValid(Customer customer)
        {
            if (String.IsNullOrEmpty(customer.LastName))
            {
                return false;
            }
            if(customer.LastName.Length < 2)
            {
                return false;
            }

            return true;
        }

        internal Int32 InsertCustomer(Customer customer, String connectionString)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String commandText = "usp_InsertCustomer";
                using(SqlCommand command = new SqlCommand(commandText))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter("customer", customer);
                    command.Parameters.Add(parameter);
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return 0;
        }

        internal void SendCustomerProcessedNotification(Customer customer)
        {
            StringBuilder message = new StringBuilder();
            message.Append(customer.FirstName + " " + customer.LastName);
            message.Append(" from " + customer.Address + " " + customer.City + ", " + customer.State);
            message.Append(" was added to our system.");
            _notifier.Notify(message.ToString());
        }

    }
}
