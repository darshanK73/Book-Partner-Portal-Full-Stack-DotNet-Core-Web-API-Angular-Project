namespace Book_Portal_API.Payloads
{
    public class TitleRequest
    {
        public string Title1 { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? PubId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public int? Royalty { get; set; }
        public int? YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime Pubdate { get; set; }
        public string[] AuIds { get; set; } = null!;
        public byte AuOrd { get; set; }
        public int Royaltyper { get; set; }
    }
}
