using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models;

public partial class Publisher : ApplicationUser
{
    [Key]
    [MaxLength(4)]
    [MinLength(4)]
    public string PubId { get; set; } = null!;

    [MaxLength(40)]
    [MinLength(5)]
    public string? PubName { get; set; }

    [Required]
    [MaxLength(20)]
    public string? City { get; set; }

    [Required]
    [MaxLength(2)]
    [MinLength(2)]
    public string? State { get; set; }

    [Required]
    [MaxLength(30)]
    [MinLength(5)]
    public string? Country { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual PubInfo? PubInfo { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
