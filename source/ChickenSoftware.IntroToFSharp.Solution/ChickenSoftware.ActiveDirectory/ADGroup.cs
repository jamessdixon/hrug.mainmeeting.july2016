using System;

namespace ChickenSoftware.ActiveDirectory
{
    public class ADGroup
    {

        public ADGroup(string accountName, string description, string userId)
        {
            if(string.IsNullOrEmpty(accountName))
            {
                throw new ArgumentException("accountName");

            }
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description");

            }
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("userId");

            }

            this.AccountName = accountName;
            this.Description = description;
            this.UserId = userId;
        }

        public string AccountName { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}