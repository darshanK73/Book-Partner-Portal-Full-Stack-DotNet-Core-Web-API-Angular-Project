using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Book_Portal_API.Models;

public partial class Author : ApplicationUser
{
    [Key]
    public string AuId { get; set; } = null!;

    [Required]
    [MaxLength(40)]
    public string AuLname { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string AuFname { get; set; } = null!;

    [Required]
    [MaxLength(12)]
    public string Phone { get; set; } = null!;

    [AllowNull]
    public string? Address { get; set; }

    [AllowNull]
    public string? City { get; set; }

    [AllowNull]
    public string? State { get; set; }

    [AllowNull]
    public string? Zip { get; set; }

    [Required]
    public bool Contract { get; set; }
    public virtual ICollection<Titleauthor> Titleauthors { get; set; } = new List<Titleauthor>();
}
