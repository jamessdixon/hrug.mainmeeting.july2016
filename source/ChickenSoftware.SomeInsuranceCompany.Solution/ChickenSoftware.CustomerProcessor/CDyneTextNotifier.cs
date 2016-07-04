using ChickenSoftware.Core;
using ChickenSoftware.Cdyne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSoftware.CustomerProcessor
{
    public class CDyneTextNotifier : INotifier
    {
        String[] _phoneNumbers = null;
        Guid _licenseKey = Guid.Empty;

        public CDyneTextNotifier(String[] phoneNumbers, Guid licenseKey)
        {
            _phoneNumbers = phoneNumbers;
            _licenseKey = licenseKey;
        }

        public void Notify(string message)
        {
            IsmsClient client = new IsmsClient("sms2wsHttpBinding");
            foreach(string phoneNumber in _phoneNumbers)
            {
                var resp = client.SimpleSMSsend(phoneNumber, message, _licenseKey);
            }
            client.Close();
        }
    }
}
