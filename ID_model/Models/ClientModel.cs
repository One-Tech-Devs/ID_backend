using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.Models
{
    internal class ClientModel : UserModel
    {
        public string Name { get; set; }
        public string? SocialName { get; set; }
        public string SSN { get; set; } //cpf
        public string NIC { get; set; } //rg
    }
}
