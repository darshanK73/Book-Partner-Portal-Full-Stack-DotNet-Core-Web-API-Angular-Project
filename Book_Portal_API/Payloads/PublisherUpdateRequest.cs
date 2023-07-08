namespace Book_Portal_API.Payloads
{
    public class PublisherUpdateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? PubName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public IFormFile Logo { get; set; }
        public string? PrInfo { get; set; }
    }
}
