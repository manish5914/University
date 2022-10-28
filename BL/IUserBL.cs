using DAL.Models;
using System.Collections.Generic;

namespace BL
{
    public interface IUserBL
    {
        List<User> GetUsers();
        int Add(User user);

    }
}