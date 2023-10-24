namespace ID_model.DTOs
{
    public class CreateCompanyDTO
    {
        public required string CompanyName { get; set; }
        public required string BusinessName { get; set; }
        public required string CorporateDocument { get; set; }
        public required string CorporateEmail { get; set; }
    }
}
