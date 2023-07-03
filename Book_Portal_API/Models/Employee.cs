using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Book_Portal_API.Models;

public partial class Employee
{
    [Key]
    [MaxLength(9)]
    [MinLength(9)]
    public string EmpId { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    [MinLength(5)]
    public string Fname { get; set; } = null!;

    [AllowNull]
    [MaxLength(1)]
    public string? Minit { get; set; }

    [Required]
    [MaxLength(30)]
    [MinLength(5)]
    public string Lname { get; set; } = null!;

    [Required]
    [MaxLength(2)]
    public short JobId { get; set; }

    [Required]
    [MaxLength(1)]
    public byte? JobLvl { get; set; }

    [Required]
    [MaxLength(4)]
    public string PubId { get; set; } = null!;

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime HireDate { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual Publisher Pub { get; set; } = null!;
}
