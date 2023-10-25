using ID_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.DTOs
{
    public class GetDataRequestDTO
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string ClientUsername { get; set; }
        public DateTime RequestCreation { get; set; }
        public DateTime RequestExpiration { get; set; }
        public string Status { get; set; }
        public string ClientData { get; set; }
    }
}
