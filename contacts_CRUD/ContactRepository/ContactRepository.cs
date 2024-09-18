using contacts_CRUD.Data.AppDbContext;
using contacts_CRUD.Dtos;
using contacts_CRUD.Entities;
using Microsoft.EntityFrameworkCore;

namespace contacts_CRUD.ContactRepository
{
    public class ContactRepository : IContactRepository
    {


        private readonly ContactDbContext _context;

        public ContactRepository(ContactDbContext dbContext)
        {
            _context = dbContext;
        }

        private async Task<bool> ContactExistAsync(ContactRequest CheckContact)
        {
            var ExistNumber = await _context.contacts.AnyAsync(b => b.PhoneNumber == CheckContact.PhoneNumber);
            if (ExistNumber)
            {
                return true;
            }
            var ExitstName = await _context.contacts.AnyAsync(b => b.Name == CheckContact.Name);
            var ExistSurname = await _context.contacts.AnyAsync(b =>b.Surname == CheckContact.Surname);
            return ExistNumber && ExitstName;
        }

        public async Task<Contact?> AddContact(ContactRequest AddContactDto)
        {
            bool ContactIsExist = await ContactExistAsync(AddContactDto);
            if (ContactIsExist)
            {
                return null;
            }

            Contact NewContact = new()
            {
                id = Guid.NewGuid(),
                PhoneNumber = AddContactDto.PhoneNumber,
                Name = AddContactDto.Name,
                Surname = AddContactDto.Surname,
                Email = AddContactDto.Email,
            };
            _context.contacts.Add(NewContact);
            await _context.SaveChangesAsync();

            return NewContact;

        }

        public async Task<bool> DeleteContact(Guid id)
        {
            var DeletedContact = await _context.contacts.FindAsync(id);
            if (DeletedContact == null)
            {
                return false;
            }
            _context.contacts.Remove(DeletedContact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _context.contacts.ToListAsync();
            
        }

        public async Task<IEnumerable<Contact>> GetContactsByPartialMatch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return  Enumerable.Empty<Contact>();
            }
            return await _context.contacts
                .Where(c => EF.Functions.Like(c.Name, $"%{searchTerm}%") ||
                    EF.Functions.Like(c.Surname, $"%{searchTerm}%") ||
                    EF.Functions.Like(c.PhoneNumber, $"%{searchTerm}%") ||
                    EF.Functions.Like(c.Email, $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<Contact?> UpdateContact(Guid id, ContactRequest Update)
        {
            var UpdatedContact = await _context.contacts.FindAsync(id);
            if (UpdatedContact == null)
            {
                return default;
            }
            UpdatedContact.Name = Update.Name;
            UpdatedContact.Surname = Update.Surname;
            UpdatedContact.Email = Update.Email;
            UpdatedContact.PhoneNumber  = Update.PhoneNumber;
            await _context.SaveChangesAsync();
            return UpdatedContact;

        }
        public async Task<Contact?> GetContactById(Guid id)
        {
            var Contact = await _context.contacts.FindAsync(id);
            if (Contact == null)
            {
                return default;
            }
            return Contact;
        }
    }
}
