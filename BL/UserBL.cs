using DataAccessLayer;
using DataAccessLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class UserBL : IUserBL
    {
        private readonly IUserDAL _userDAL;
        public UserBL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }
        public List<User> GetUsers()
        {
            return _userDAL.GetUsers();
        }
        public int Add(User user)
        {
            user.Salt = Encryption.GetRandomSalt();
            user.Password = Encryption.HashPassword(user.Password, user.Salt);
            return _userDAL.Add(user);
        }
        public User AuthenticateUser(User user)
        {
            var userByEmail = _userDAL.GetUserByEmail(user).FirstOrDefault();
            if (userByEmail == null)
            {
                return null;
            }
            if (AuthenticatePassword(user, userByEmail)){
                return userByEmail;
            }
            return null; 
        }
        private static bool AuthenticatePassword(User user, User userByEmail)
        {
            return (Encryption.HashPassword(user.Password, userByEmail.Salt) == userByEmail.Password) ? true : false;
        }
        public string GetUserIDByEmail(User user)
        {
            var userByEmail = _userDAL.GetUserByEmail(user).FirstOrDefault();
            return userByEmail.ID.ToString();
        }
        public string GetStudents()
        {
            return JsonConvert.SerializeObject(_userDAL.GetStudents());
        }
        public int ApproveStudents(int[] students)
        {
            return _userDAL.UpdateStudent(ArrayToString(students));
        }
        private string ArrayToString(int[] students)
        {
            string studentParameter = "";
            for(int index = 0; index < students.Length; index++)
            {
                studentParameter += students[index] + ",";
            }
            return studentParameter.TrimEnd(',');
        }
        public string RedirectUser(User user)
        {
            if (user == null)
            {
                return "Login";
            }
            if (user.RoleId == (int)Roles.Admin)
            {
                return "Admin";
            }
            if (user.RoleId == (int)Roles.User)
            {
                if (_userDAL.GetStudentCountByUserId(user) == 0)
                {
                    return "../Student/Register";
                }
                return "../Student/Detail";
            }
            return "Login";
        }
    }
}
