namespace ID_model.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }   
        public AccessInformation AccessInformation { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid AddressId { get; set; }
        public AddressModel? Address { get; set; }
    }
}
