using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccessLayer;
using DAL.Models;
using Newtonsoft.Json;


namespace BL
{
    public class StudentBL
    {
        StudentDAL studentDAL = new StudentDAL();
        public void Add(Student student)
        {
            studentDAL.Add(student);
            studentDAL.AddResult(student);
        }

        public string GetSubjects()
        {
            return JsonConvert.SerializeObject(studentDAL.GetSubjects());
        }
        public string GetGrades()
        {
            return JsonConvert.SerializeObject(studentDAL.GetGrades());
        }
    }
}
