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
            var contacts = _context.Contacts.Include(c => c.PhoneNumbers).ToList();
            var response = new StringBuilder();
            foreach (var contact in contacts)
            {
                response.AppendLine("----------------------");
                response.AppendLine($"Name: {contact.FullName}");
                response.AppendLine($"Office Address: {contact.BusinessAddress}");
                foreach (var phone in contact.PhoneNumbers)
                    response.AppendLine($"Tel: {phone.PhoneNumber}");
                response.AppendLine($"Home Address: {contact.HomeAddress}");
            }

            return Ok(response.ToString());
        }

        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return CreatedAtRoute("GetContact", new { id = contact.Id }, contact);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.Include(c => c.PhoneNumbers).FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            _context.PhoneNumbers.RemoveRange(contact.PhoneNumbers);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return NoContent();
        }
    }
}