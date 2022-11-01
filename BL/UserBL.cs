﻿using DataAccessLayer;
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
        public string RedirectUser(User authenticatedUser)
        {
            if (authenticatedUser == null)
            {
                return "Login";
            }
            if (authenticatedUser.RoleId == (int)Roles.Admin)
            {
                return "Admin";
            }
            else if (authenticatedUser.RoleId == (int)Roles.User)
            {
                return "../Student/StudentDetails";
            }
            else
            {
                return "Welcome";
            }
        }
    }
}
