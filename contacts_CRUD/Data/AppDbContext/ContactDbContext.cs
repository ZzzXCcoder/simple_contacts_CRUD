using contacts_CRUD.Entities;
using Microsoft.EntityFrameworkCore;

namespace contacts_CRUD.Data.AppDbContext
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> contacts => Set<Contact>();
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) 
        {
        
        }

    }
}

