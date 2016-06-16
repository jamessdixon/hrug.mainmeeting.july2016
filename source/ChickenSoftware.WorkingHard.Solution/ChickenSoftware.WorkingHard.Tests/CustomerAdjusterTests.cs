using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChickenSoftware.WorkingHard.Tests
{
    [TestClass]
    public class CustomerAdjusterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithNull_ThrowsException()
        {
            var adjuster = new CustomerAdjuster(null);
            Assert.Fail("Should have gotten ArgumentNull Exception");
        }

        public void ChangeOrderCount_ReturnsExpected()
        {
            var customer = new Customer();
            customer.NumberOfOrders = 5;
            var adjuster = new CustomerAdjuster(customer);
            adjuster.ChangeOrderCount(2);
            var actual = adjuster.Customer.NumberOfOrders;
            var expected = 7;
            Assert.AreEqual(actual, expected);
        }
    }
}
