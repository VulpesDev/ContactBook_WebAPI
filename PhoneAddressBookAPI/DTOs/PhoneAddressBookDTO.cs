namespace PhoneAddressBookAPI.DTOs
{
    public class ContactDTO
    {
        public string FullName { get; set; }
        public List<AddressDTO> Addresses { get; set; }
    }

    public class PhoneAddressBookDTO
    {
        public string FullName { get; set; }
        public List<AddressDTO> Addresses { get; set; }
    }

    public class AddressDTO
    {
        public string Addr { get; set; }
        public bool IsBusinessAddress { get; set; }
        public List<string> PhoneNumbers { get; set; }
    }
}
