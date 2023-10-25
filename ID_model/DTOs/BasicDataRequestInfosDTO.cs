namespace ID_model.DTOs
{
    public class BasicDataRequestInfosDTO
    {
        public Guid Id { get; set; }    
        public Guid ClientId { get; set; }   
        public string CompanyName { get; set; }
        public string ClientData { get; set; }
        public DateTime RequestCreationDate { get; set; }
        public DateTime RequestExpirationDate { get; set; }
        public string Status { get; set; }
    }
}

