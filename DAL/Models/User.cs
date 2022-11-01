using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;


namespace DataAccessLayer.Models
{
    public class User
    {
        public int ID { get; set; } 
        public string Email { get; set; }   
        public string Password { get; set; }
        public string Salt {get; set; }
        public int RoleId { get; set; }
        public User()
        {

        }

        public User(int iD, string email, string password, string salt, int roleId)
        {
            ID = iD;
            Email = email;
            Password = password;
            Salt = salt;
            RoleId = roleId;
        }   
    }
}