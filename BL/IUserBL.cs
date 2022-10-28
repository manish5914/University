using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IUserBL
    {
        List<User> GetUsers();
        int Add(User user);

    }
}