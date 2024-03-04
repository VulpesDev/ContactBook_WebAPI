using System.Collections.Generic;

namespace PhoneAddressBookAPI.Models
{
    public class Contact
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string HomeAddress { get; set; } = string.Empty;
    public string BusinessAddress { get; set; } = string.Empty;
    public List<string> PhoneNumbers { get; set; } = new List<string>();
}

}
