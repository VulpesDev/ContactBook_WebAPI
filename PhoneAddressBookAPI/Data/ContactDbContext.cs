using Microsoft.EntityFrameworkCore;
using PhoneAddressBookAPI.Models;

namespace PhoneAddressBookAPI.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<List<string>>().HasNoKey();

        //     base.OnModelCreating(modelBuilder);
        // }
    }
}
