using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.Models
{
    internal class CompanyModel : UserModel
    {
        public string CompanyName { get; set; }
        public string BusinessName { get; set; }
        public string CorporateDocument { get; set; }
        public bool StatusRF { get; set; } = false;
    }
}
