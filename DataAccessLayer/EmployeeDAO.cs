using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDAO
    {
        public IQueryable<EmployeeDTO> GetEmployees()
        {
            FuminiHotelProjectPrn212Context context = new FuminiHotelProjectPrn212Context();
            return context.Employees
                .Join(context.EmployeeRoles,
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
        public static Employee? GetEmployeeById(int id) 
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            return _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }
        public static Employee? GetEmployeeByEmail(string email)
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            return _context.Employees.FirstOrDefault(e => e.Email == email);
        }
        public static void AddEmployee(Employee NewEmployee) 
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            _context.Employees.Add(NewEmployee);
            _context.SaveChanges();

        }
        public static void UpdateEmployee(Employee employee)
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            _context.Employees.Update(employee);
            _context.SaveChanges();

        }
        public static void DeleteEmployee(int id)
        {

            using (var context = new FuminiHotelProjectPrn212Context()) 
            {
                var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                if (employee != null)
                {
                    if (employee.UserId.HasValue)
                    {
                        var user = context.Users.FirstOrDefault(u => u.UserId == employee.UserId.Value);
                        if (user != null) 
                        {
                            context.Users.Remove(user);
                        }
                    }                   
                }
                context.Employees.Remove(employee);
                context.SaveChanges();
            }

        }
    }
}
