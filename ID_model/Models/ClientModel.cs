namespace ID_model.Models
{
    public class ClientModel : UserModel
    {
        public string Name { get; set; }
        public string? SocialName { get; set; }
        public string SSN { get; set; } //cpf
        public string NIC { get; set; } //rg
        public string SecurityPhrase { get; set; }
    }
}
