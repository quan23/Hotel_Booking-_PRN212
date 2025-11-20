using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }
        public bool CheckEmailExists(string email) => userRepository.CheckEmailExists(email);
      

        public bool Login(string username, string password) => userRepository.Login(username, password);
       

        public bool Register(string email, string role, string status, string fullname, string telephone, string password, DateTime? birthday) => userRepository.Register(email, role, status, fullname, telephone, password, birthday);

        public bool IsAdmin(string email) => userRepository.IsAdmin(email);

        public bool IsEmployee(string email) => userRepository.IsEmployee(email);

        public bool ChangePassword(int userId, string oldPassword, string newPassword) => userRepository.ChangePassword(userId, oldPassword, newPassword);

        public User? GetUserById(int id)=> userRepository.GetUserById(id);
    }
}
