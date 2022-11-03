using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer
{
    public interface IUserDAL
    {
        int Add(User user);
        List<User> GetUsers();
        List<User> GetUserByEmail(User user);
        DataTable GetStudents();
        int UpdateStudent(string student);
        int GetStudentCountByUserId(User user);
    }
}