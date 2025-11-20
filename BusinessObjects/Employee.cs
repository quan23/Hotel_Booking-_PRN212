using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public string? FullName { get; set; }

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public DateTime HireDate { get; set; }

    public decimal? Salary { get; set; }

    public string? Status { get; set; }

    public virtual EmployeeRole? Role { get; set; }

    public virtual User? User { get; set; }
}
