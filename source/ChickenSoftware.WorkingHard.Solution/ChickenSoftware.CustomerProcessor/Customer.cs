using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProcessor.CS
{
    public class Customer
    {
        public Int32 CustomerId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
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
