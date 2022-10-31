using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Schema;
using BCrypt = BCrypt.Net.BCrypt;

namespace BusinessLayer
{
    public class UserBL : IUserBL
    {
        UserDAL userDAL = new UserDAL();
        
        public UserBL()
        {

        }
        public List<User> GetUsers()
        {
            return userDAL.GetUsers();
        }
        public int Add(User user)
        {
            user.Salt = Encryption.GetRandomSalt();
            user.Password = Encryption.HashPassword(user.Password, user.Salt);
            return userDAL.Add(user);
        }
        public User AuthenticateUser(User user)
        {
            var userByEmail = userDAL.GetUserByEmail(user).FirstOrDefault();
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
            var userByEmail = userDAL.GetUserByEmail(user).FirstOrDefault();
            return userByEmail.ID.ToString();
        }
    }
}
