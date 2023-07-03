using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models;

public partial class Discount
{
    [Required]
    [MaxLength(40)]
    public string Discounttype { get; set; } = null!;

    [Required]
    [MaxLength(4)]
    public string? StorId { get; set; }

    [Required]
    [MaxLength(2)]
    public short? Lowqty { get; set; }

    [Required]
    [MaxLength(2)]
    public short? Highqty { get; set; }

    [Required]
    [MaxLength(5)]
    public decimal Discount1 { get; set; }

    public virtual Store? Stor { get; set; }
}
