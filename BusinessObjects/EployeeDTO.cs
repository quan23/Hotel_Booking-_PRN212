using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? RoleName { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public DateTime HireDate { get; set; }
        public string? Status { get; set; }
        public decimal? Salary { get; set; }
    }
}
