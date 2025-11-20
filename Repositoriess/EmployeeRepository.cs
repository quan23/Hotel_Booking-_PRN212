using BusinessObjects;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void AddEmployee(Employee NewEmployee) => EmployeeDAO.AddEmployee(NewEmployee);
    

        public void DeleteEmployee(int id) => EmployeeDAO.DeleteEmployee(id);
       

        public Employee? GetEmployeeById(int id) => EmployeeDAO.GetEmployeeById(id);

        public Employee? GetEmployeeByEmail(string email) => EmployeeDAO.GetEmployeeByEmail(email);

        public IQueryable<EmployeeDTO> GetEmployees()
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            return _context.Employees
                .Join(_context.EmployeeRoles, 
                      e => e.RoleId, 
                      r => r.RoleId, 
                      (e, r) => new EmployeeDTO 
                      {
                          EmployeeId = e.EmployeeId,
                          FullName = e.FullName,
                          RoleName = r.RoleName, 
                          Email = e.Email,
                          Telephone = e.Telephone,
                          HireDate = e.HireDate,
                          Status = e.Status,
                          Salary = e.Salary
                      }).AsQueryable();
        }

        public void UpdateEmployee(Employee employee) => EmployeeDAO.UpdateEmployee(employee);
       
    }
}
