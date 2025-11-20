using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool CheckEmailExists(string email) => UserDAO.CheckEmailExists(email);
      

        public bool Login(string username, string password) => UserDAO.Login(username, password);
     

        public bool Register(string email, string role, string status, string fullname, string telephone, string password, DateTime? birthday) => UserDAO.Register(email, role, status, fullname, telephone, password, birthday);

        public bool IsAdmin(string email) => UserDAO.IsAdmin(email);

        public bool IsEmployee(string email) => UserDAO.IsEmployee(email);

        public bool ChangePassword(int userId, string oldPassword, string newPassword)=> UserDAO.ChangePassword(userId, oldPassword, newPassword);

        public User? GetUserById(int id)=> UserDAO.GetUserById(id);
    }
}
