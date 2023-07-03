using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models
{
    public class ApplicationUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
