using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models;

public partial class PubInfo
{
    [Key]
    public string PubId { get; set; } = null!;
    public byte[]? Logo { get; set; }

    [Required]
    public string? PrInfo { get; set; }

    public virtual Publisher Pub { get; set; } = null!;
}
