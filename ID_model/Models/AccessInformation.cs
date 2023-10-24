using System.ComponentModel.DataAnnotations.Schema;

namespace ID_model.Models
{
    [NotMapped]
    public class AccessInformation
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
