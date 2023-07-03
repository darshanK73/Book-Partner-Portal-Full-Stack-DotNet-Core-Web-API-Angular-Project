using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Book_Portal_API.Models;

public partial class Title
{
    [Key]
    public string TitleId { get; set; } = null!;

    [Required]
    public string Title1{ get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;

    [Required]
    public string? PubId { get; set; }

    [Required]
    public decimal? Price { get; set; }

    [Required]
    public decimal? Advance { get; set; }

    [Required]
    public int? Royalty { get; set; }

    [Required]
    public int? YtdSales { get; set; }

    [Required]
    public string? Notes { get; set; }

    [Required]
    public DateTime Pubdate { get; set; }
    public virtual Publisher? Pub { get; set; }
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Titleauthor> Titleauthors { get; set; } = new List<Titleauthor>();
}
