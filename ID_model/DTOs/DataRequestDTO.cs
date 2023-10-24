using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.DTOs
{
    public class DataRequestDTO
    {
        public Guid CompanyId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime RequestExpiration { get; set; }
        public string[] ClientData { get; set; }
    }
}
