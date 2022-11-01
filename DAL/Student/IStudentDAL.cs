using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public interface IStudentDAL
    {
        DataTable GetSubjects();
        DataTable GetGrades();
        void AddStudent(Student student);

    }
}
