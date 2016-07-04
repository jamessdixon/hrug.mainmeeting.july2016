using ChickenSoftware.Core;
using System;
using System.Net.Mail;


namespace ChickenSoftware.CustomerProcessor
{
    public class EMailNotifier : INotifier
    {
        string _smtpAddress = String.Empty;
        string[] _addresses = null;

        public EMailNotifier(string[] addresses, String smtpAddress)
        {
            _smtpAddress = smtpAddress;
            _addresses = addresses;
        }
        public void Notify(string message)
        {
            using (SmtpClient client = new SmtpClient(_smtpAddress))
            {
                foreach(string address in _addresses)
                {
                    MailMessage mailMessage = new MailMessage("postmaster@aetna.com", address, "A message from Chicken Software", message);
                    client.Send(mailMessage);
                }
            }
        }
    }
}