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
        private readonly IStudentDAL _studentDAL;
        public StudentBL(IStudentDAL studentDAL)
        {
            _studentDAL = studentDAL;
        }
        public int Add(Student student)
        {
            student.Status = DetermineStatus(student.Grades);
            return _studentDAL.AddStudent(student);
        }
        public string DetermineStatus(char[] results)
        {
            int totalGrade = 0;
            Dictionary<char, int> grades = new Dictionary<char, int>();
            DataTable gradeDT = _studentDAL.GetGrades();
            foreach (DataRow dr in gradeDT.Rows)
            {
                grades.Add(Convert.ToChar(dr[0]), Convert.ToInt32(dr[1]));
            }
            foreach (char grade in results)
            {
                grades.TryGetValue(grade, out int gradeValue);
                totalGrade += gradeValue;
            }
            if (totalGrade < 10)
            {
                return "Rejected";
            }
            else
            {
                return "Waiting";
            }
        }
        public string GetSubjects()
        {
            return JsonConvert.SerializeObject(_studentDAL.GetSubjects());
        }
        public string GetGrades()
        {
            return JsonConvert.SerializeObject(_studentDAL.GetGrades());
        }
        public Student GetStudent(Student student)
        {
            return _studentDAL.GetStudent(student).FirstOrDefault();
        }
    }
}
