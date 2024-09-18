using contacts_CRUD.ContactRepository;
using contacts_CRUD.Dtos;
using contacts_CRUD.Entities;

namespace contacts_CRUD.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _contactRepository.GetAllContacts();
        }

        public async Task<Contact?> GetContactById(Guid id)
        {
            return await _contactRepository.GetContactById(id);
        }

        public async Task<IEnumerable<Contact>> GetContactsByPartialMatch(string searchTerm)
        {
            return await _contactRepository.GetContactsByPartialMatch(searchTerm);
        }

        public async Task<bool> AddContact(ContactRequest contactRequest)
        {
            if (contactRequest.Name == null && contactRequest.PhoneNumber == null)
            {
                return false;
            }
            var newContact = await _contactRepository.AddContact(contactRequest);
            return newContact != null;
        }

        public async Task<Contact> UpdateContact(Guid id, ContactRequest contactDto)
        {
            return await _contactRepository.UpdateContact(id, contactDto);
        }

        public async Task<bool> DeleteContact(Guid id)
        {
            return await _contactRepository.DeleteContact(id);
        }
    }
}
