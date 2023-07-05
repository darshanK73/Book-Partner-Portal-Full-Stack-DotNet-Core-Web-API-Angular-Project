namespace Book_Portal_API.Payloads
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
