using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
//using System.Web.SessionState;
using System.Data;
using DAL.Models;
using System.Diagnostics;

namespace DAL.DataAccessLayer
{
    public class StudentDAL
    {
       
        DBConnection dbconnection;
        public StudentDAL()
        {
            dbconnection = new DBConnection();
        }
        public void Add(Student student)
        {
            dbconnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand($"insert into Students(NID, FirstName, LastName, Email, PhoneNumber, GuardianName, DateOfBirth) " +
                $"values('{student.NID}', '{student.FirstName}', '{student.LastName}','{student.Email}','{student.PhoneNumber}','{student.GuardianName}','{student.DateOfBirth}'); ", dbconnection.connection);
            
            int executed = sqlCommand.ExecuteNonQuery();
 
           
            
            dbconnection.CloseConnection();
        }
        public void AddResult(Student student)
        { 
            dbconnection.OpenConnection();
            SqlCommand sqlCommand;
           
            var studentId = GetStudent(student.NID);
            if (studentId != null)
            {
                if (student.Subjects.Length == student.Grades.Length)
                {
                    for (int i = 0; i < student.Subjects.Length; i++)
                    {
                        sqlCommand = new SqlCommand($"insert into Results(StudentId, SubjectId, Grade) values('{studentId}','{student.Subjects[i]}','{student.Grades[i]}')", dbconnection.connection);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            dbconnection.CloseConnection(); 

        }
        public string GetStudent(string studentId)
        {
            string query = $"select StudentId from Students where NID = '{studentId}'";
            var students =  DAL.GetData(query);
            return students.Rows.Count == 1 ? Convert.ToString(students.Rows[0][0]) : null;
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
    }
}