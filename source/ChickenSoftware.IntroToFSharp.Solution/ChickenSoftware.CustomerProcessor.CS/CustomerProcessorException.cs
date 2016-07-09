using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProcessor.CS
{
    [Serializable]
    public class CustomerProcessorException : Exception
    {
        public CustomerProcessorException() { }
        public CustomerProcessorException(string message) : base(message) { }
        public CustomerProcessorException(string message, Exception inner) : base(message, inner) { }
        protected CustomerProcessorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
