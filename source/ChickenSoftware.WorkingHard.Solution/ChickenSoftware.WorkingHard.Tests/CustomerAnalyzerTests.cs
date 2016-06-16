using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChickenSoftware.WorkingHard.Tests
{
    [TestClass]
    public class CustomerAnalyzerTests
    {
        [TestMethod]
        public void ShouldCustomerGetACouponUsingCary_ReturnsTrue()
        {
            var mock = new Mock<Customer>();
            mock.Setup(Customer => Customer.City).Returns("Cary");
            var analyzer = new CustomerAnalyzer();
            var expected = true;
            var actual = analyzer.ShouldCustomerGetACoupon(mock.Object);
            Assert.AreEqual(expected, actual);
        }
    }
}
