using System.ComponentModel.DataAnnotations;

namespace API.Entity
{
    public class Auth
    {
        public int id { get; set; }
        [Required]
        public string userName { get; set; }
        public string Password { get; set; }
    }
}