using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using NLog;

namespace DataAccessLayer
{


    public class DBConnection : IDBConnection
    {

        private static Logger logger = LogManager.GetLogger("myLoggerRule");
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
            try
            {
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
            }catch(Exception execption)
            {
                logger.Error(execption.Message);
            }
           
        }
        public void CloseConnection()
        {
            try
            {
                if(connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch(Exception exception)
            {
                logger.Error(exception.Message);
            }
        }
    }
}