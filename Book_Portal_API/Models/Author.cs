using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Book_Portal_API.Models;

public partial class Author : ApplicationUser
{
    [Key]
    [MaxLength(11)]
    [MinLength(11)]
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

    [Required]
    [MaxLength(40)]
    public string? Address { get; set; }

    [Required]
    [MaxLength(20)]
    public string? City { get; set; }

    [Required]
    [MaxLength(2)]
    [MinLength(2)]
    public string? State { get; set; }

    [Required]
    [MaxLength(5)]
    [MinLength(5)]
    public string? Zip { get; set; }

    [Required]
    public bool Contract { get; set; }
    public virtual ICollection<Titleauthor> Titleauthors { get; set; } = new List<Titleauthor>();
}
