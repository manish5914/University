using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
//using System.Web.SessionState;
using System.Data;
using DAL.Models;

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
                $"values('{student.NID}', '{student.FirstName}', '{student.LastName}','{student.Email}','{student.PhoneNumber}','{student.GuardianName}','{student.DateOfBirth}'); " +
                $"insert into Results(NID, Subject1,Grade1, Subject2,Grade2, Subject3, Grade3) " +
                $"values('{student.NID}','{student.Subjects[0]}','{student.Grades[0]}','{student.Subjects[1]}','{student.Grades[1]}','{student.Subjects[2]}','{student.Grades[2]}')"
                , dbconnection.connection);
            int executed = sqlCommand.ExecuteNonQuery();

            dbconnection.CloseConnection();
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