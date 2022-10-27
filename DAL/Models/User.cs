using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DAL.Models
{
    public class User
    {
        public int ID { get; set; } 
        public string Email { get; set; }   
        public string Password { get; set; }

        public User()
        {

        }

        public User(int iD, string email, string password)
        {
            ID = iD;
            Email = email;
            Password = password;
        }   
    }
}