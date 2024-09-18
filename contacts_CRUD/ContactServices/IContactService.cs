using contacts_CRUD.Dtos;
using contacts_CRUD.Entities;

namespace contacts_CRUD.ContactServices
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact?> GetContactById(Guid id);
        Task<IEnumerable<Contact>> GetContactsByPartialMatch(string searchTerm);
        Task<bool> AddContact(ContactRequest contactDto);
        Task<Contact> UpdateContact(Guid id, ContactRequest contactDto);
        Task<bool> DeleteContact(Guid id);

    }
}
