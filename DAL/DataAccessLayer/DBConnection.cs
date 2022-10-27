using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL.DataAccessLayer
{
    public class DBConnection
    {
        string dbconnection = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public SqlConnection connection;

        public DBConnection()
        {
            connection = new SqlConnection(dbconnection);
            OpenConnection();
        }
        public void OpenConnection()
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
        }
        public void CloseConnection()
        {
            if(connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}