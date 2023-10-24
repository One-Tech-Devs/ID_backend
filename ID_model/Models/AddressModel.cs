using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.Models
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string StateOrProvince { get; set; } = string.Empty;
        public string CityOrVillage { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
    }
}
