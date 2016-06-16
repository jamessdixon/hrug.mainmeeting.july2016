using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProcessor.CS
{
    [Serializable]
    public class NotificationException : Exception
    {
        public NotificationException() { }
        public NotificationException(string message) : base(message) { }
        public NotificationException(string message, Exception inner) : base(message, inner) { }
        protected NotificationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
