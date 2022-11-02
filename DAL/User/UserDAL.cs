using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using DataAccessLayer.Models;
using System.Configuration;

namespace DataAccessLayer
{
    public class UserDAL: IUserDAL
    {
        private readonly IDBConnection _dbConnection;
        public UserDAL(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public int Add(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserID", user.ID));
            parameters.Add(new SqlParameter("@Email", user.Email));
            SqlParameter sqlPasswordParameter = new SqlParameter("@Password", Encoding.UTF8.GetBytes(user.Password));
            sqlPasswordParameter.SqlDbType = SqlDbType.VarBinary;
            parameters.Add(sqlPasswordParameter); 
            SqlParameter sqlSaltParameter = new SqlParameter("@Salt", Encoding.UTF8.GetBytes(user.Salt));
            sqlSaltParameter.SqlDbType = SqlDbType.VarBinary;
            parameters.Add(sqlSaltParameter);
            return DAL.InsertUpdateData(SqlCommands.InsertUser, parameters);
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            DataTable dataTable = DAL.GetData(SqlCommands.GetUsers);
            if(dataTable != null)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    users.Add(new User(
                        Convert.ToInt32(row[0]),
                        Convert.ToString(row[1]),
                        Convert.ToString(row[2]),
                        Convert.ToString(row[3]),
                        Convert.ToInt32(row[4])
                        ));
                }
                _dbConnection.CloseConnection();
                return users;
            }
            return null;
        }
        public List<User> GetUserByEmail(User user)
        {
            List<User> users = new List<User>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", user.Email));
            DataTable dataTable = DAL.GetDataWithParameters(SqlCommands.GetUserByEmail, parameters);
            if(dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    users.Add(new User(
                        Convert.ToInt32(row[0]),
                        user.Email,
                        Convert.ToString(Encoding.UTF8.GetString((byte[])row[1])),
                        Convert.ToString(Encoding.UTF8.GetString((byte[])row[2])),
                        Convert.ToInt32(row[3])
                    )); 
                }
                _dbConnection.CloseConnection();
                return users;
            }
            return null;
        }
        public DataTable GetStudents()
        { 
            return DAL.GetData(SqlCommands.GetStudentsWithTotalGrade);
        }
        public int UpdateStudent(string students)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = SqlCommands.ApproveStudents +"("+ students+")";
            return DAL.InsertUpdateData(query, parameters);
        }
    }
}