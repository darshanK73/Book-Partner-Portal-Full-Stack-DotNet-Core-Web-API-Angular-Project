using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models;

public partial class Sale
{
    [Key]
    public string StorId { get; set; } = null!;

    [Required]
    public string OrdNum { get; set; } = null!;

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime OrdDate { get; set; }

    [Required]
    public short Qty { get; set; }

    [Required]
    public string Payterms { get; set; } = null!;

    [Required]
    public string TitleId { get; set; } = null!;

    public virtual Store Stor { get; set; } = null!;

    public virtual Title Title { get; set; } = null!;
}
