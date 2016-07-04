using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProcessor.CS
{
    /// <summary>
    /// This represents a customer
    /// </summary>
    public class Customer
    {
        //This is the First Name
        public Int32 CustomerId { get; set; }
        //This is the LastName
        public String FirstName { get; set; }
        //This is the LastName
        public String LastName { get; set; }
        //This is a waste of time
        public String Address { get; set; }
        //Seriously, you need XML comments for this?
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
    }

    public enum MyEnum
    {
        UnknownError = -2,
        OutOfDiskSpaceError = -1,
        StillRunning = 0,
        Successful = 1
    }
}
