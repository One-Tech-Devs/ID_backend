namespace ID_model.Models
{
    public class DataRequestModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public Guid ClientId { get; set; }
        public ClientModel Client { get; set; }
        public DateTime RequestCreation { get; set; }
        public DateTime RequestExpiration { get; set; }
        public string Status { get; set; }
        public string ClientData { get; set; } // TO DO: Decidir como receber checklist de dados
    }
}
