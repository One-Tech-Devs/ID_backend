using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.DTOs
{
    public class LoginDTO
    {
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
