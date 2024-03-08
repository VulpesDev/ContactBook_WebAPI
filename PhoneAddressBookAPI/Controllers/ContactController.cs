using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneAddressBookAPI.Data;
using PhoneAddressBookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using PhoneAddressBookAPI.DTOs;




namespace PhoneAddressBookAPI.Controllers
{
    // public class ContactViewModel
    // {
    //     public string FullName { get; set; }
    //     public AddressViewModel Address { get; set; }
    //     public string PhoneNumber { get; set; }
    // }

    // public class AddressViewModel
    // {
    //     public string Addr { get; set; }
    //     public bool IsBusinessAddress { get; set; }
    // }

    public class ContactViewModel
    {
        public string FullName { get; set; }
        public List<AddressViewModel> Addresses { get; set; } = new List<AddressViewModel>();
    }

    public class AddressViewModel
    {
        public string Addr { get; set; }
        public bool IsBusinessAddress { get; set; }
        public List<string> PhoneNumbers { get; set; } = new List<string>();
    }

    

    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ContactDbContext _context;

        public ContactController(ILogger<ContactController> logger, ContactDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetContact")]
        public ActionResult<string> Get()
        {
            var contacts = _context.Contacts
                .Include(c => c.ContactAddresses)
                    .ThenInclude(ca => ca.Address)
                        .ThenInclude(a => a.PhoneNumbers)
                .ToList();

            var response = new StringBuilder();
            foreach (var contact in contacts)
            {
                response.AppendLine("----------------------");
                response.AppendLine($"Name: {contact.FullName}");

                foreach (var contactAddress in contact.ContactAddresses)
                {
                    var address = contactAddress.Address;
                    response.AppendLine($"{(address.IsBusinessAddress ? "Office Address" : "Home Address")}: {address.Addr}");

                    foreach (var phoneNumber in address.PhoneNumbers)
                    {
                        response.AppendLine($"Tel: {phoneNumber.PhoneNumber}");
                    }
                }

                response.AppendLine("----------------------");
            }

            return Ok(response.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var contact = new Contacts { FullName = model.FullName };
            _context.Contacts.Add(contact);
            foreach (var addressModel in model.Addresses)
            {
                var address = new Addresses { Addr = addressModel.Addr, IsBusinessAddress = addressModel.IsBusinessAddress };
                foreach (var phoneNumber in addressModel.PhoneNumbers)
                {
                    var phone = new PhoneNum { PhoneNumber = phoneNumber };
                    _context.PhoneNum.Add(phone);
                    address.PhoneNumbers.Add(phone);
                }
                _context.Addresses.Add(address);

                await _context.SaveChangesAsync();

                var contactAddress = new ContactAddresses 
                {
                    ContactId = contact.Id,
                    AddressId = address.Id
                };
                _context.ContactAddresses.Add(contactAddress);
                contact.ContactAddresses.Add(contactAddress);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();

            return Ok("Contact created successfully");
        }
    }
}