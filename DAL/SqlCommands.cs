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

        public const string GetStudents = "select StudentId, NID, FirstName, LastName, PhoneNumber, DateOfBirth, Email, GuardianName, UserId, Status from Students";
        public const string GetStudentByUserId = "select StudentId, NID, FirstName, LastName, PhoneNumber, DateOfBirth, Email, GuardianName, UserId, Status from Students where UserId = @UserId";
        public const string GetStudentsWithTotalGrade = @"select s.studentid,s.nid, s.firstname, s.lastname, s.email, s.phonenumber, s.dateofbirth, s.guardianname, s.userid, s.status, Total.TotalGrade from 
		                                                    (select s.studentid, Sum(g.GradeValue ) as TotalGrade
		                                                    from Results as r join 
		                                                    Grades as g 
			                                                    on r.Grade = g.Grade
		                                                    join Students as s
			                                                    on s.studentId = r.studentId
		                                                    group by (s.studentid)) as Total 
	                                                    join Students as s 
	                                                    on Total.studentid = s.studentid
	                                                    order by Total.TotalGrade desc";
        public const string GetUsers = "select UserId, Email, Password, Salt, RoleId from Users";
        public const string ApproveStudents = "update students set status = 'Approved' where studentid in ";
        public const string GetUserByEmail = "select UserId, Password, Salt, RoleId from Users where Email = @Email";
        public const string GetStudentCountByUserID = "select count(studentid) from students where userid = @UserID";

    }
}
