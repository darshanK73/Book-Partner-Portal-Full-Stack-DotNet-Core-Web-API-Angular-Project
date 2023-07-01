using System;
using System.Collections.Generic;

namespace Book_Portal_API.Models;

public partial class Job
{
    public short JobId { get; set; }

    public string JobDesc { get; set; } = null!;

    public byte MinLvl { get; set; }

    public byte MaxLvl { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
