using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IUserDAL
    {
        int Add(User user);
        List<User> GetUsers();
        List<User> GetUserByEmail(User user);

    }
}