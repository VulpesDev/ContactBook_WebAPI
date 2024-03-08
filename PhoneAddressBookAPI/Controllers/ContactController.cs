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
    public class ContactViewModel
    {
        public string FullName { get; set; }
        public AddressViewModel Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class AddressViewModel
    {
        public string Addr { get; set; }
        public bool IsBusinessAddress { get; set; }
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
            var address = new Addresses { Addr = model.Address.Addr, IsBusinessAddress = model.Address.IsBusinessAddress };
            
            _context.Contacts.Add(contact);
            _context.Addresses.Add(address);
            
            await _context.SaveChangesAsync(); // Save changes to the database to obtain Ids
            
            var contactAddress = new ContactAddresses 
            {
                ContactId = contact.Id, // Assuming contact has been added to the database and has an assigned Id
                AddressId = address.Id // Assuming address has been added to the database and has an assigned Id
            };

            _context.ContactAddresses.Add(contactAddress);
            
            await _context.SaveChangesAsync();
            var phone = new PhoneNum { PhoneNumber = model.PhoneNumber, AddressId = address.Id }; // Assign AddressId
            _context.PhoneNum.Add(phone);
            // Save changes to the database
            await _context.SaveChangesAsync();
            
            return Ok("Contact created successfully");
            
        }



        // [HttpPost]
        // public IActionResult Post(Contacts contact)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     _context.Contacts.Add(contact);
        //     _context.SaveChanges();

        //     return CreatedAtRoute("GetContact", new { id = contact.Id }, contact);
        // }

        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     var contact = _context.Contacts.Include(c => c.PhoneNumbers).FirstOrDefault(c => c.Id == id);
        //     if (contact == null)
        //     {
        //         return NotFound();
        //     }
        //     _context.PhoneNumbers.RemoveRange(contact.PhoneNumbers);
        //     _context.Contacts.Remove(contact);
        //     _context.SaveChanges();

        //     return NoContent();
        // }
    }
}