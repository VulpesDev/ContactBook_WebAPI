using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneAddressBookAPI.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string HomeAddress { get; set; } = string.Empty;
        public string BusinessAddress { get; set; } = string.Empty;

        public ICollection<PhoneNum> PhoneNumbers { get; set; } = new List<PhoneNum>();
    }

    public class PhoneNum
    {
        [Key]
        public int PhoneNumberId { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
    }
}
