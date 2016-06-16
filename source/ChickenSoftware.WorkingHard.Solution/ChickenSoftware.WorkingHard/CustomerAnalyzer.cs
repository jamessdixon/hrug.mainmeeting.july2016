using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSoftware.WorkingHard
{
    public class CustomerAnalyzer
    {
        public bool ShouldCustomerGetACoupon(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            switch (customer.City)
            {
                case "Cary":
                    return true;
                case "Durham":
                    return true;
                default:
                    return false;
            }
        }
    }
}
