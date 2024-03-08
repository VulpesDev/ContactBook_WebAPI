using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneAddressBookAPI.Data;
using PhoneAddressBookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace PhoneAddressBookAPI.Controllers
{

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

            //Build a formated response
            var response = new StringBuilder();
            foreach (var contact in contacts)
            {
                response.AppendLine("----------------------");
                response.AppendLine($"Id: {contact.Id}");
                response.AppendLine($"Name: {contact.FullName}");

                foreach (var contactAddress in contact.ContactAddresses)
                {
                    var address = contactAddress.Address;
                    response.AppendLine($"{(address.IsBusinessAddress ? "Office Address" : "Home Address")}: {address.Addr}");
                    foreach (var phoneNumber in address.PhoneNumbers)
                        response.AppendLine($"Tel: {phoneNumber.PhoneNumber}");
                }
                response.AppendLine("----------------------");
            }
            return Ok(response.ToString());
        }

        [HttpPost(Name = "PostContact")]
        public async Task<IActionResult> CreateContact([FromBody] ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingContact = await _context.Contacts
                .Include(c => c.ContactAddresses)
                .ThenInclude(ca => ca.Address)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingContact == null)
                return NotFound();

            existingContact.FullName = model.FullName;

            _context.ContactAddresses.RemoveRange(existingContact.ContactAddresses);

            foreach (var addressModel in model.Addresses)
            {
                var address = new Addresses { Addr = addressModel.Addr, IsBusinessAddress = addressModel.IsBusinessAddress };
                foreach (var phoneNumber in addressModel.PhoneNumbers)
                {
                    var phone = new PhoneNum { PhoneNumber = phoneNumber };
                    _context.PhoneNum.Add(phone);
                    address.PhoneNumbers.Add(phone);
                }
                existingContact.ContactAddresses.Add(new ContactAddresses { Address = address });
                _context.Addresses.Add(address);
            }

            await _context.SaveChangesAsync();

            return Ok("Contact updated successfully");
        }

        [HttpDelete(Name ="{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                return NotFound();

            var contactAddresses = await _context.ContactAddresses.Where(ca => ca.ContactId == id).ToListAsync();
            _context.ContactAddresses.RemoveRange(contactAddresses);

            _context.Contacts.Remove(contact);

            await _context.SaveChangesAsync();

            return Ok("Contact deleted successfully");
        }
    }
}