using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer
{
    public interface IUserBL
    {
        List<User> GetUsers();
        int Add(User user);
        string GetStudents();
        User AuthenticateUser(User user);
        string RedirectUser(User authenticatedUser);
        void ApproveStudents(int[] students);

    }
}