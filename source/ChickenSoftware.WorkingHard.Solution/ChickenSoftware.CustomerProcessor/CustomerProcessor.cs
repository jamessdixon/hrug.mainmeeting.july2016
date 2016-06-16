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
    public class CustomerProcessor
    {
        /// <summary>
        /// This method persists the customer to the datastore and then emails
        /// interested parties if the insert was successful.
        /// </summary>
        /// <param name="customer">A customer</param>
        /// <returns>A customer number.</returns>
        /// <exception cref="ArgumentNullException">Customer cannot be null.</exception>
        /// <exception cref="CustomerProcessorException">An exception occured that prevented the operation to occur.</exception>
        public Int32 RegisterNewCustomer(Customer customer)
        {
            Int32 customerId = 0;
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            try
            {
                if (IsCustomerValid(customer))
                {
                    try
                    {
                        String connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                        try
                        {
                            customerId = InsertCustomer(customer, connectionString);
                            customer.CustomerId = customerId;
                            try
                            {
                                String manager = ConfigurationManager.AppSettings["managerEMail"];
                                String director = ConfigurationManager.AppSettings["directorEMail"];
                                try
                                {
                                    EmailCustomerProcessedNotification(customer, manager);
                                    EmailCustomerProcessedNotification(customer, director);
                                }
                                catch(ArgumentNullException exception)
                                {
                                    throw new CustomerProcessorException("customer can not be null.", exception);
                                }
                                catch(NotificationException exception)
                                {
                                    throw new CustomerProcessorException("Database insert worked but email notification failed.", exception);
                                }
                            }
                            catch (ConfigurationErrorsException exception)
                            {
                                throw new CustomerProcessorException("configuration file read failed", exception);
                            }
                        }
                        catch(ArgumentNullException exception)
                        {
                            throw new CustomerProcessorException("customer can not be null.", exception);
                        }
                        catch(InsertCustomerException exception)
                        {
                            throw new CustomerProcessorException("Failed to write to datastore.", exception);
                        }
                    }
                    catch(ConfigurationErrorsException exception)
                    {
                        throw new CustomerProcessorException("configuration file read failed.", exception);
                    }
                }
            }
            catch(ArgumentNullException exception)
            {
                throw new CustomerProcessorException("customer can not be null.", exception);
            }
            return customerId;
        }

        internal bool IsCustomerValid(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException("customer");
            }

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
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            if (customer == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String commandText = "usp_InsertCustomer";
                using(SqlCommand command = new SqlCommand(commandText))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter("customer", customer);
                    command.Parameters.Add(parameter);
                    try
                    {
                        sqlConnection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch(InvalidCastException exception)
                        {
                            throw new InsertCustomerException("Could not execute command successfully.", exception);
                        }
                        catch (SqlException exception)
                        {
                            throw new InsertCustomerException("Could not execute command successfully.", exception);
                        }
                        catch (IOException exception)
                        {
                            throw new InsertCustomerException("Could not execute command successfully.", exception);
                        }
                        catch (InvalidOperationException exception)
                        {
                            throw new InsertCustomerException("Could not execute command successfully.", exception);
                        }
                    }
                    catch(InvalidOperationException exception)
                    {
                        throw new InsertCustomerException("Could not make a successful connection.", exception);
                    }
                    catch (SqlException exception)
                    {
                        throw new InsertCustomerException("Could not make a successful connection.", exception);
                    }
                    catch (ConfigurationErrorsException exception)
                    {
                        throw new InsertCustomerException("Could not make a successful connection.", exception);
                    }
                }
            }
            return 0;
        }

        internal void EmailCustomerProcessedNotification(Customer customer, String emailReceipiant)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            String subject = "Customer " + customer.CustomerId + "was added";

            StringBuilder message = new StringBuilder();
            message.Append(customer.FirstName + " " + customer.LastName);
            message.Append(" from " + customer.Address + " " + customer.City + ", " + customer.State);
            message.Append(" was added to our system.");

            try
            {
                String hostName = ConfigurationManager.AppSettings["smtpAddress"];
                using(SmtpClient client = new SmtpClient(hostName))
                {
                    MailMessage mailMessage = new MailMessage("postmaster@aetna.com",emailReceipiant,subject,message.ToString());
                    try
                    {
                        client.Send(mailMessage);
                    }
                    catch(ArgumentNullException exception)
                    {
                        throw new NotificationException("Could not execute email successfully.", exception);
                    }
                    catch (InvalidOperationException exception)
                    {
                        throw new NotificationException("Could not execute email successfully.", exception);
                    }
                    catch (SmtpException exception)
                    {
                        throw new NotificationException("Could not execute email successfully.", exception);
                    }
                }
            }
            catch (ConfigurationErrorsException exception)
            {
                throw new CustomerProcessorException("Configuration file read failed.", exception);
            }
        }

    }
}
