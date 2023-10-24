namespace ID_model.Models
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; } 
        public string StateOrProvince { get; set; } 
        public string CityOrVillage { get; set; }
        public string PostalCode { get; set; }
        public string Neighborhood { get; set; } 
        public string Street { get; set; } 
        public string Number { get; set; } 
    }
}
