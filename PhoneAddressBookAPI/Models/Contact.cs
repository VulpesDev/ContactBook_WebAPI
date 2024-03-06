using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneAddressBookAPI.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string HomeAddress { get; set; } = string.Empty;
        public string BusinessAddress { get; set; } = string.Empty;

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
    }

    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
