using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProcessor.CS
{
    public class CustomerProcessor_Improper
    {
        public Int32 RegisterNewCustomer(Customer customer)
        {
            Int32 customerId = 0;
            if (IsCustomerValid(customer))
            {
                String connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                customerId = InsertCustomer(customer, connectionString);
                customer.CustomerId = customerId;
                String manager = ConfigurationManager.AppSettings["managerEMail"];
                String director = ConfigurationManager.AppSettings["directorEMail"];
                EmailCustomerProcssedNotification(customer, manager);
                EmailCustomerProcssedNotification(customer, director);
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

        internal void EmailCustomerProcssedNotification(Customer customer, String emailReceipiant)
        {
            String subject = "Customer " + customer.CustomerId + "was added";
            StringBuilder message = new StringBuilder();
            message.Append(customer.FirstName + " " + customer.LastName);
            message.Append(" from " + customer.Address + " " + customer.City + ", " + customer.State);
            message.Append(" was added to our system.");
            String hostName = ConfigurationManager.AppSettings["smtpAddress"];
            using(SmtpClient client = new SmtpClient(hostName))
            {
                MailMessage mailMessage = new MailMessage("postmaster@aetna.com",emailReceipiant,subject,message.ToString());
                    client.Send(mailMessage);
            }
        }

    }
}
