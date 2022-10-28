using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAL.Models;

namespace DAL.DataAccessLayer
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
             
            SqlCommand sqlCommand = new SqlCommand($"insert into Users(Email, Password) values('{user.Email}','{user.Password}')", dbconnection.connection);
            int executed = sqlCommand.ExecuteNonQuery();
            dbconnection.CloseConnection();
            return executed;
            
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            SqlCommand sqlCommand = new SqlCommand($"select * from Users", dbconnection.connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dbconnection.OpenConnection();
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach(DataRow row in dataTable.Rows)
            {
                users.Add(new User(
                    Convert.ToInt32(row[0]),
                    Convert.ToString(row[1]),
                    Convert.ToString(row[2])
                    ));

            }
            dbconnection.CloseConnection();
            return users;
        }
        public List<User> GetUserByEmail(User user)
        {
            List<User> users = new List<User>();
            SqlCommand sqlCommand = new SqlCommand($"select * from Users where Email = '{user.Email}'");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                users.Add(new User(
                    Convert.ToInt32(row[0]),
                    Convert.ToString(row[1]),
                    Convert.ToString(row[2])
                    ));
            }
            dbconnection.CloseConnection();
            return users;
        }
    }
}