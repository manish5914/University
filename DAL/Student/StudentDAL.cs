using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Models;
using System.Diagnostics;
using NLog;

namespace DataAccessLayer
{
    public class StudentDAL : IStudentDAL
    {
        private readonly IDBConnection _dbConnection;
        private static Logger logger = LogManager.GetLogger("myLoggerRule");
        public StudentDAL(IDBConnection dBConnection)
        {
            _dbConnection = dBConnection;
        }
        public List<Student> GetStudent(Student student)
        {
            List<Student> students = new List<Student>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("UserId", student.UserId));
            DataTable datatable = DAL.GetDataWithParameters(SqlCommands.GetStudentByUserId, parameters);
            if(datatable != null)
            {
                foreach(DataRow row in datatable.Rows)
                {
                    students.Add(new Student(
                        Convert.ToInt32(row[0]),
                        Convert.ToString(row[1]),
                        Convert.ToString(row[2]),
                        Convert.ToString(row[3]),
                        Convert.ToString(row[4]),
                        Convert.ToDateTime(row[5]),
                        Convert.ToString(row[6]),
                        Convert.ToString(row[7]),
                        Convert.ToInt32(row[8]),
                        Convert.ToString(row[9])
                        )) ;
                }
                _dbConnection.CloseConnection();
                return students;
            }
            return null;
        }
        public DataTable GetSubjects()
        {
            return DAL.GetData(SqlCommands.GetSubjects);
        }
        public DataTable GetGrades()
        {
            return DAL.GetData(SqlCommands.GetGrades);
        }
        public int AddStudent(Student student)
        {
            try
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
                SqlCommand sqlcommand = new SqlCommand("RegisterStudent", _dbConnection.connection);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@results", resultsTable);
                sqlcommand.Parameters.AddWithValue("@students", studentsTable);
                _dbConnection.OpenConnection();
                int rowAffected = sqlcommand.ExecuteNonQuery();
                _dbConnection.CloseConnection();
                logger.Info("Registered Student");
                return rowAffected;
            }catch(Exception exception)
            {
                logger.Error(exception.Message);
                return UniversityConstants.rowAffectedZero;
            }
            
        }
    }
}