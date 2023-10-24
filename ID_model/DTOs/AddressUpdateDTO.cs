using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.DTOs
{
    public class AddressUpdateDTO
    {
        public string Country { get; set; }
        public string StateOrProvince { get; set; }
        public string CityOrVillage { get; set; }
        public string PostalCode { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
    }
}
