using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Portal_API.Models;

public partial class Job
{
    [Key]
    [MaxLength(2)]
    [MinLength(2)]
    public short JobId { get; set; }

    [Required]
    [MaxLength(50)]
    public string JobDesc { get; set; } = null!;

    [Required]
    [MaxLength(1)]
    public byte MinLvl { get; set; }

    [Required]
    [MaxLength(1)]
    public byte MaxLvl { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
