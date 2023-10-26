namespace ID_model.DTOs
{
    public class DataRequestDTO
    {
        public required string CompanyUsername { get; set; }
        public required string CompanyName { get; set; }
        public required string BusinessName { get; set; }
        public required string ClientUsername { get; set; }
        public required DateTime RequestExpiration { get; set; }
        public required string[] ClientData { get; set; }
    }
}
