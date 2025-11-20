using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        bool Login(string username, string password);
        bool Register(string email, string role, string status, string fullname, string telephone, string password, DateTime? birthday);
        bool CheckEmailExists(string email);
        bool IsAdmin(string email);
        bool IsEmployee(string email);
        bool ChangePassword(int userId, string oldPassword, string newPassword);
        User? GetUserById(int id);
    }
}
