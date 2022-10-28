using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DAL
    {
       
        public static DataTable GetData(string query)
        { 
            DBConnection dbconnection = new DBConnection();
            dbconnection.OpenConnection();
            List<string> subjects = new List<string>();
            SqlCommand sqlcommand = new SqlCommand(query, dbconnection.connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            dbconnection.CloseConnection();
            return dataTable;
        }
    }
}
