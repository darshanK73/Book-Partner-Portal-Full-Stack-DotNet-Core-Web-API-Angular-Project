namespace Book_Portal_API.Models
{
    public class FileUpload
    {
        public IFormFile file { get; set; }
        public string FileName { get; set; }
        public string PubId { get; set; }
    }
}
