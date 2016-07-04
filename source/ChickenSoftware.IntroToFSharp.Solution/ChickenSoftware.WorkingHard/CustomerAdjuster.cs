using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSoftware.WorkingHard
{
    public class CustomerAdjuster
    {
        Customer _customer = null;
        public CustomerAdjuster(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            _customer = customer;
        }

        public Customer Customer
        {
            get
            {
                return _customer;
            }
        }

        public void ChangeOrderCount(Int32 numberOfOrders)
        {
            _customer.NumberOfOrders = numberOfOrders;
        }
    }
}
