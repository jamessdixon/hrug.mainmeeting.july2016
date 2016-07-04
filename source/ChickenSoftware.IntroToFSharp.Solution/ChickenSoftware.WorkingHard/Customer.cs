using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSoftware.WorkingHard
{
    public class Customer
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String StreetAddress { get; set; }
        public virtual String City { get; set; }
        public String State { get; set; }
        public String PostalCode { get; set; }
        public Int32 NumberOfOrders { get; set; }

    }
}
