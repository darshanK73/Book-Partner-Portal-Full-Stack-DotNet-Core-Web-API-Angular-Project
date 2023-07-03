using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models;

public partial class Store
{
    [Key]
    public string StorId { get; set; } = null!;

    [Required]
    public string? StorName { get; set; }

    [Required]
    public string? StorAddress { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    public string? State { get; set; }

    [Required]
    public string? Zip { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
