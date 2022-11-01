using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccessLayer
{


    public class DBConnection : IDBConnection
    {
        string dbconnection = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public SqlConnection connection { get; set; }


        public DBConnection()
        {
           string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
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