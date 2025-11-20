using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace FUMiniHotel_ProjectPRN212.Profile
{
    /// <summary>
    /// Interaction logic for EmployeeProfile.xaml
    /// </summary>
    
    public partial class EmployeeProfile : Page
    {
        private BusinessObjects.Employee _employee;
        private readonly EmployeeService employeeService;
        private readonly FuminiHotelProjectPrn212Context context;

        public EmployeeProfile(BusinessObjects.Employee employee)
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            context = new FuminiHotelProjectPrn212Context();
            _employee = employee;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            // Load lại employee với thông tin Role
            var employeeWithRole = context.Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.EmployeeId == _employee.EmployeeId);

            if (employeeWithRole != null)
            {
                _employee = employeeWithRole;
            }

            txtFullName.Text = _employee.FullName ?? "N/A";
            txtPhone.Text = _employee.Telephone ?? "N/A";
            txtEmail.Text = _employee.Email ?? "N/A";
            txtHireDate.Text = _employee.HireDate.ToString("dd/MM/yyyy");
            txtRole.Text = _employee.Role?.RoleName ?? "N/A";
            txtSalary.Text = _employee.Salary?.ToString("N0") + " VND" ?? "N/A";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var editProfile = new EditEmployeeProfile(_employee);
            if (editProfile.ShowDialog() == true)
            {
                // Load lại dữ liệu sau khi chỉnh sửa
                _employee = employeeService.GetEmployeeById(_employee.EmployeeId);
                LoadEmployeeData();
            }
        }
    }
}

