using System;
using System.Collections.Generic;

namespace Book_Portal_API.Models;

public partial class Titleview
{
    public string Title { get; set; } = null!;

    public byte? AuOrd { get; set; }

    public string AuLname { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? YtdSales { get; set; }

    public string? PubId { get; set; }
}
