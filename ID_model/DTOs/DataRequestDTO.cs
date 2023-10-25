namespace ID_model.DTOs
{
    public class DataRequestDTO
    {
        public string CompanyUsername { get; set; }
        public string ClientUsername { get; set; }
        public DateTime RequestExpiration { get; set; }
        public string[] ClientData { get; set; }
    }
}
