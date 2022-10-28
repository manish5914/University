using DataAccessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            return userDAL.Add(user);
        }
    }
}
