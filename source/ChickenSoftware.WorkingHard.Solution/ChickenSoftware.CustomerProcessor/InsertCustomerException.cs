using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProcessor.CS
{
    [Serializable]
    public class InsertCustomerException : CustomerProcessorException
    {
        public InsertCustomerException() { }
        public InsertCustomerException(string message) : base(message) { }
        public InsertCustomerException(string message, Exception inner) : base(message, inner) { }
        protected InsertCustomerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
