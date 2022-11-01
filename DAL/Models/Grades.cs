using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Models
{ 
    public class Grade
    { 
        public char Id { get; set; }
        public int GradeValue { get; set; }

        public Grade()
        {

        }
        public Grade(char grade, int gradeValue)
        {
            Id = grade;
            GradeValue = gradeValue;
        }   
    }
}