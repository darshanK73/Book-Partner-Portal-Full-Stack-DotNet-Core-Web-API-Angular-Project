using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models;

public partial class Roysched
{
    [Required]
    public string TitleId { get; set; } = null!;

    [Required]
    public int? Lorange { get; set; }

    [Required]
    public int? Hirange { get; set; }

    [Required]
    public int? Royalty { get; set; }

    public virtual Title Title { get; set; } = null!;
}
