using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAO
    {
        //login
        public static bool Login(string email,  string password)
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user != null;
        }
        //kiem tra xem co phai la admin khong 
        public static bool IsAdmin(string email) 
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) { return false; }
            return _context.Admins.Any(u => u.Email == email);
        }
        //kiem tra xem co phai la employee khong 
        public static bool IsEmployee(string email) 
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) { return false; }
            return _context.Employees.Any(u => u.Email == email);
        }
        public static bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            using (var _context = new FuminiHotelProjectPrn212Context())
            {
                var userWithCustomer = _context.Users
                                               .Join(_context.Customers,
                                                     user => user.UserId,
                                                     customer => customer.UserId,
                                                     (user, customer) => new { User = user, Customer = customer })
                                               .SingleOrDefault(uc => uc.User.UserId == userId);

                if (userWithCustomer == null)
                {
                    throw new Exception("Không tìm thấy tài khoản người dùng.");
                }

                var user = userWithCustomer.User;
                int customerId = userWithCustomer.Customer.CustomerId; 

                if (user.Password != oldPassword)
                {
                    throw new Exception("Mật khẩu cũ không chính xác.");
                }

                if (!IsValidPassword(newPassword))
                {
                    throw new Exception("Mật khẩu mới phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.");
                }

                user.Password = newPassword; 
                _context.SaveChanges();
                return true;
            }
        }

        private static bool IsValidPassword(string password)
        {
            // Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }

        public static User? GetUserById(int id)
        {
            using( var _context = new FuminiHotelProjectPrn212Context())
            {
                return _context.Users.SingleOrDefault(u => u.UserId == id);
            } 
        }
        //Check mail xem co trong database chua
        public static bool CheckEmailExists(string email)
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            return _context.Users.Any(u => u.Email == email);
        }
        //register
        public static bool Register(string email, string role, string status, string fullname, string telephone, string password, DateTime? birthday)
        {
            FuminiHotelProjectPrn212Context _context = new FuminiHotelProjectPrn212Context();
            if (CheckEmailExists(email))
            {
                return false;
            }
            //them vao bang user
            var newUser = new User()
            {
                Email = email,
                Role = role,
                Status = status,
                Password = password,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //lay userid vua duoc tao
            int newUserId = newUser.UserId;
             
            //them vao bang customers        
            var newCustomer = new Customer()
            {
                UserId = newUserId,              
                FullName = fullname,
                Telephone = telephone,
                Email = email,
                Birthday = birthday,
                Status = status,
            };
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return true;
        }
    }
}
