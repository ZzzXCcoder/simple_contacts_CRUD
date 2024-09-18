using System.ComponentModel.DataAnnotations;

namespace contacts_CRUD.Dtos
{
    public record class ContactRequest(
        [StringLength (30)] string Name,
        [StringLength (30)] string? Surname,
        [StringLength (11)] string PhoneNumber,
        [StringLength (50)] string? Email
    );
   
}
