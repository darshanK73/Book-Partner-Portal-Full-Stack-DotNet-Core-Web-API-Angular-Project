namespace Book_Portal_API.Payloads
{
    public class TitleauthorPublishRequest
    {
        public string au_id { get; set; }
        public string title_id { get; set; }
        public int au_ord { get; set; }
        public int royaltyper { get; set; }
    }
}
