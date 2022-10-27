using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccessLayer;
using DAL.Models;

namespace BL
{
    public class StudentBL
    {
        StudentDAL studentDAL = new StudentDAL();
        public void Add(Student student)
        {
            studentDAL.Add(student);
        }

        public DataTable GetSubjects()
        {
            return studentDAL.GetSubjects();
        }
        public DataTable GetGrades()
        {
            return studentDAL.GetGrades();
        }
    }
}
