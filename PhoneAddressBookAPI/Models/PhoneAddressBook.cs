using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneAddressBookAPI.Models
{
    public class Contacts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        
        public ICollection<ContactAddresses> ContactAddresses { get; set; } = new List<ContactAddresses>();
    }

    public class Addresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Addr { get; set; } = string.Empty;
        public bool IsBusinessAddress { get; set; }

        public ICollection<PhoneNum> PhoneNumbers { get; set; } = new List<PhoneNum>();
    }

    public class ContactAddresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Contacts")]
        public int ContactId { get; set; }
        public Contacts Contact { get; set; }

        [ForeignKey("Addresses")]
        public int AddressId { get; set; }
        public Addresses Address { get; set; }
    }

    public class PhoneNum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneNumberId { get; set; }
        
        public string PhoneNumber { get; set; } = string.Empty;

        [ForeignKey("Addresses")]
        public int AddressId { get; set; }
        public Addresses Address { get; set; }
    }
}
