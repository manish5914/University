using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Models
{ 
    public class Grades
    { 
        public char Grade { get; set; }
        public int GradeValue { get; set; }

        public Grades()
        {

        }
        public Grades(char grade, int gradeValue)
        {
            Grade = grade;
            GradeValue = gradeValue;
        }   
    }
}