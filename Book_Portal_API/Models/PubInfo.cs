using System;
using System.Collections.Generic;

namespace Book_Portal_API.Models;

public partial class PubInfo
{
    public string PubId { get; set; } = null!;

    public byte[]? Logo { get; set; }

    public string? PrInfo { get; set; }

    public virtual Publisher Pub { get; set; } = null!;
}
