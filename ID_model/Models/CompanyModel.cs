namespace ID_model.Models
{
    public class CompanyModel : UserModel
    {
        public string CompanyName { get; set; }
        public string BusinessName { get; set; }
        public string CorporateDocument { get; set; }
        public bool StatusRF { get; set; } = false;
        public Guid AddressId { get; set; }
        public AddressModel? Address { get; set; }
    }
}
