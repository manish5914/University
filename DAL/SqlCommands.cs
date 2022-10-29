using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class SqlCommands
    {
        public const string RegisterStudent = "insert into Students(NID, FirstName, LastName, Email, PhoneNumber, GuardianName, DateOfBirth) values(@NID, @FirstName, @LastName, @Email, @PhoneNumber, @GuardianName, @DateOfBirth);";
        public const string GetStudent = "select StudentId from Students where NID = @NID";
        public const string GetSubjects = "select SubjectId, SubjectName from Subjects";
        public const string GetGrades = "select Grade, GradeValue from Grades";

    }
}
