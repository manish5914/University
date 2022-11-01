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
        DBConnection dbconnection;
        public UserDAL()
        {
            dbconnection = new DBConnection();
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
            try
            {
                DAL.InsertUpdateData(SqlCommands.InsertUser, parameters);
                return 1;
            }
            catch (Exception exception)
            {
                throw;
            }   
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            DataTable dataTable = DAL.GetData(SqlCommands.GetUsers);
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
            dbconnection.CloseConnection();
            return users;
        }
        public List<User> GetUserByEmail(User user)
        {
            List<User> users = new List<User>();
            SqlCommand sqlCommand = new SqlCommand($"select UserId, Password, Salt, RoleId from Users where Email = '{user.Email}'", dbconnection.connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
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
            dbconnection.CloseConnection();
            return users;
        }
        public DataTable GetStudents()
        { 
            return DAL.GetData(SqlCommands.GetStudents);
        }
    }
}