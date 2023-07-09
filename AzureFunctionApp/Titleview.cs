using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionApp
{
    public class Titleview
    {
        public string title_id { get; set; } = null!;
        public string title { get; set; } = null!;
        public string type { get; set; } = null!;
        public string? pub_id { get; set; }
        public decimal? price { get; set; }
        public decimal? advance { get; set; }
        public int? royalty { get; set; }
        public int? ytd_sales { get; set; }
        public string? notes { get; set; }
        public DateTime pubdate { get; set; }
        public List<Titleauthor> titleauthors { get; set; }
    }
}
