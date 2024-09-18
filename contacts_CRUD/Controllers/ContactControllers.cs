using Microsoft.AspNetCore.Mvc;
using contacts_CRUD.ContactServices;
using contacts_CRUD.Dtos;
using contacts_CRUD.Entities;
namespace contacts_CRUD.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactContollers : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactContollers(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        [Route("AddContact")]
        public async Task<ActionResult<bool>> AddContact(ContactRequest AddedContact)
        {

            var NewContactIsCreated = await _contactService.AddContact(AddedContact);
            if (NewContactIsCreated)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }

        }
        [HttpGet]
        [Route("GetAllContacts")]
        public async Task<ActionResult<List<Contact>>> GetAllContacts()
        {
            var ContactList = await _contactService.GetAllContacts();
            if (ContactList != null)
            {
                return Ok(ContactList);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetContactById/{id}")]
        public async Task<ActionResult<ContactResponse>> GetContactById(Guid id)
        {
            var contact = await _contactService.GetContactById(id);

            if (contact != null)
            {
                var contactResponse = new ContactResponse(
                    contact.Name,
                    contact.Surname,
                    contact.PhoneNumber,
                    contact.Email
                );
                return Ok(contactResponse);
            }

            return NotFound();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ContactResponse>>> GetContactsByPartialMatch(string searchTerm)
        {
            var contacts = await _contactService.GetContactsByPartialMatch(searchTerm);
            if (!contacts.Any())
            {
                return NotFound();
            }
            var contactsResponce = contacts.Select(contact => new ContactResponse(contact.Name, contact.Surname, contact.PhoneNumber, contact.Email));

            return Ok(contactsResponce);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactResponse>> UpdateContact(Guid id, ContactRequest contactRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); 
                }

                var updatedContact = await _contactService.UpdateContact(id, contactRequest);

                if (updatedContact == null)
                {
                    return NotFound(); 
                }

                var contactResponse = new ContactResponse(
                    updatedContact.Name,
                    updatedContact.Surname,
                    updatedContact.PhoneNumber,
                    updatedContact.Email
                );

                return Ok(contactResponse); 
            }
            catch (Exception ex)
            {
                // Логирование ошибки (можно использовать ILogger)
                return StatusCode(500, "An error occurred while processing your request."); 
            }
        }
        [HttpDelete]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(Guid id)
        {
            var IsDeleted = await _contactService.DeleteContact(id);
            if (IsDeleted)
            {
                return Ok("Удален");
            }
            return NoContent();
        }

    }

}
