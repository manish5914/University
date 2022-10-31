using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using Newtonsoft.Json;

namespace BusinessLayer
{
    public class StudentBL : IStudentBL
    {
        IStudentDAL _studentDAL;
        public StudentBL()
        {
            _studentDAL = new StudentDAL();
        }
        public void Add(Student student)
        {
            //_studentDAL.Add(student);
            //_studentDAL.AddResult(student);
            _studentDAL.AddStudent(student);
        }
        public string GetSubjects()
        {
            return JsonConvert.SerializeObject(_studentDAL.GetSubjects());
        }
        public string GetGrades()
        {
            return JsonConvert.SerializeObject(_studentDAL.GetGrades());
        }
    }
}
