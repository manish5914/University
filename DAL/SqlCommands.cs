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
        public const string GetStudent = "select StudentId from Students where NID = @NID";
        public const string GetSubjects = "select SubjectId, SubjectName from Subjects";
        public const string GetGrades = "select Grade, GradeValue from Grades";
        public const string InsertUser = "Begin Transaction; insert into Users(Email, Password, Salt) values(@Email, @Password, @Salt); commit;";

        public const string GetStudents = "select StudentId, NID, FirstName, LastName, PhoneNumber, DateOfBirth, GuardianName, UserId, Status from Students";
        public const string GetUsers = "select UserId, Email, Password, Salt, RoleId from Users";
    }
}
