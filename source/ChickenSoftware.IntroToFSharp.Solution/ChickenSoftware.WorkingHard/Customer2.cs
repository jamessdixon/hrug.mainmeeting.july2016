using System;
using System.ComponentModel.DataAnnotations;

namespace ChickenSoftware.WorkingHard
{
    public class Customer2
    {
        [Required]
        public Guid Id { get; set; }
        [MinLength(2)]
        [MaxLength(30)]
        public String FirstName { get; set; }
        [MinLength(2)]
        [MaxLength(30)]
        public String LastName { get; set; }
        [MaxLength(100)]
        public String StreetAddress { get; set; }
        [MaxLength(100)]
        public String City { get; set; }
        [MaxLength(100)]
        public String State { get; set; }
        [MaxLength(100)]
        public String PostalCode { get; set; }
    }
}
