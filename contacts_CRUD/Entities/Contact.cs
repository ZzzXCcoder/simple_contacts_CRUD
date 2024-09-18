namespace contacts_CRUD.Entities
{
    public class Contact
    {
        public Guid id { get; set; }
        public string? Name { get; set; } = null;

        public string? Surname { get; set; } = null;

        public string? PhoneNumber {  get; set; }

        public string? Email { get; set; }


    }
}
