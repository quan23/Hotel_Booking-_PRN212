using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class EmployeeRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
