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
            dbconnection.OpenConnection();
            List<string> subjects = new List<string>();
            SqlCommand sqlcommand = new SqlCommand($"select SubjectId, SubjectName from Subjects", dbconnection.connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                subjects.Add(Convert.ToString(row[0]));
            }
            dbconnection.CloseConnection();
            return dataTable;
        }
        public DataTable GetGrades()
        {
            dbconnection.OpenConnection();
            List<Grades> grades = new List<Grades>();
            SqlCommand sqlCommand = new SqlCommand($"select Grade, GradeValue from Grades", dbconnection.connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach(DataRow row in dataTable.Rows)
            {
                grades.Add(new Grades(Convert.ToChar(row[0]), Convert.ToInt32(row[1])));
            }
            dbconnection.CloseConnection();
            return dataTable;
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
            studentsTable.Rows.Add(student.NID, student.FirstName, student.LastName, student.Email, student.PhoneNumber, student.DateOfBirth, student.GuardianName, student.UserId);
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