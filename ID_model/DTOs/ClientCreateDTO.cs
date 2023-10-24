namespace ID_model.DTOs
{
    public class ClientCreateDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
        public required string SecurityPhrase { get; set; }
        public string? SocialName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string SSN { get; set; }
        public string? NIC { get; set; }
    }
}
