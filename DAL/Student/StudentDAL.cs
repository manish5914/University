using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Models;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class StudentDAL : IStudentDAL
    {
        DBConnection dbconnection;
        public StudentDAL()
        {
            dbconnection = new DBConnection();
        }
        public DataTable GetSubjects()
        {
            return DAL.GetData(SqlCommands.GetSubjects);
        }
        public DataTable GetGrades()
        {
            return DAL.GetData(SqlCommands.GetGrades);
        }
        public void AddStudent(Student student)
        {
            DataTable studentsTable = new DataTable();
            DataTable resultsTable = new DataTable();
            studentsTable.Columns.Add(new DataColumn("NID", typeof(string)));
            studentsTable.Columns.Add(new DataColumn("FirstName", typeof(string)));
            studentsTable.Columns.Add(new DataColumn("LastName", typeof(string)));
            studentsTable.Columns.Add(new DataColumn("Email", typeof(string)));
            studentsTable.Columns.Add(new DataColumn("PhoneNumber", typeof(string)));
            studentsTable.Columns.Add(new DataColumn("DateOfBirth", typeof(DateTime)));
            studentsTable.Columns.Add(new DataColumn("GuardianName", typeof(string)));
            studentsTable.Columns.Add(new DataColumn("UserId", typeof(int)));
            studentsTable.Columns.Add(new DataColumn("Status", typeof(string)));
            studentsTable.Rows.Add(student.NID, student.FirstName, student.LastName, student.Email, student.PhoneNumber, student.DateOfBirth, student.GuardianName, student.UserId, student.Status);
            resultsTable.Columns.Add(new DataColumn("SubjectId", typeof(int)));
            resultsTable.Columns.Add(new DataColumn("Grade", typeof(char)));
            if (student.Subjects.Length == student.Grades.Length)
            {
                for(var index = 0; index < student.Subjects.Length; index++)
                {
                    resultsTable.Rows.Add(student.Subjects[index], student.Grades[index]);
                }
                
            }
            SqlCommand sqlcommand = new SqlCommand("RegisterStudent", dbconnection.connection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@results", resultsTable);
            sqlcommand.Parameters.AddWithValue("@students", studentsTable);
            dbconnection.OpenConnection();
            sqlcommand.ExecuteNonQuery();
            dbconnection.CloseConnection();
        }
    }
}