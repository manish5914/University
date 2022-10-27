using DAL.DataAccessLayer;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class UserBL
    {
        UserDAL userDAL = new UserDAL();
   
        public List<User> GetUsers()
        {
            return userDAL.GetUsers();
        }
        public int Add(User user)
        {
            return userDAL.Add(user);
        }
    }
}
