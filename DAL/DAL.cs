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
        public static void InsertUpdateData(string query, List<SqlParameter> parameters, DBConnection dbconnection)
        {
            using (SqlCommand cmd = new SqlCommand(query, dbconnection.connection))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                cmd.ExecuteNonQuery();
            }
            dbconnection.CloseConnection();
        }
        public static DataTable GetDataQurery(string query, DBConnection dbconnection)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, dbconnection.connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            dbconnection.CloseConnection();

            return dt;
        }
    }
   
}
