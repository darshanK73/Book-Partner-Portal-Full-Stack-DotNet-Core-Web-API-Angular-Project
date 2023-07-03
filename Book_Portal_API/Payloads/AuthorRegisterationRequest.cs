using Book_Portal_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Payloads
{
    public class AuthorRegisterationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AuLname { get; set; } = null!;
        public string AuFname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
     
    }
}
