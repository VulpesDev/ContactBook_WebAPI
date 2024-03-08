using Microsoft.EntityFrameworkCore;
using PhoneAddressBookAPI.Models;

namespace PhoneAddressBookAPI.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }

        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<ContactAddresses> ContactAddresses { get; set; }
        public DbSet<PhoneNum> PhoneNum { get; set; }
    }
}
