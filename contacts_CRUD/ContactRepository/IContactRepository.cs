using contacts_CRUD.Dtos;
using contacts_CRUD.Entities;

namespace contacts_CRUD.ContactRepository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact?> AddContact(ContactRequest contact);

        Task<Contact?> UpdateContact(Guid id, ContactRequest contact);

        Task<bool> DeleteContact(Guid id);

        public  Task<IEnumerable<Contact>> GetContactsByPartialMatch(string searchTerm);

        public Task<Contact?> GetContactById(Guid id);



    }
}
