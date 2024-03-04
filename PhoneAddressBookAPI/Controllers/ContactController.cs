using Microsoft.AspNetCore.Mvc;
using PhoneAddressBookAPI.Models;

namespace PhoneAddressBookAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetContact")]
    public Contact Get()
    {
        return new Contact {
            Id = 0,
            FullName = "Peter Peterson",
            HomeAddress = "Schoneweide str. 32",
            BusinessAddress = "Harzer str. 11",
            PhoneNumbers = new List<string> {"359-111-232", "49-222-111"}
        };
    }
}
