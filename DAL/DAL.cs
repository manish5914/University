using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using Serilog;

namespace DataAccessLayer
{
    public static class DAL
    {
        public static int InsertUpdateData(string query, List<SqlParameter> parameters)
        {
            try
            {
                DBConnection dbconnection = new DBConnection();
                dbconnection.OpenConnection();
                int rowAffected;
                using (SqlCommand cmd = new SqlCommand(query, dbconnection.connection))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        parameters.ForEach(parameter => {
                            cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                        });
                    }
                    rowAffected = cmd.ExecuteNonQuery();
                }
                dbconnection.CloseConnection();
                return rowAffected;
            }
            catch (Exception exception)
            {                
                Debug.WriteLine(exception.Message);
                return UniversityConstants.rowAffectedZero;  
            }
        }
        public static DataTable GetData(string query)
        {
            try
            {
                DBConnection dbconnection = new DBConnection();
                dbconnection.OpenConnection();
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
            }catch(Exception exception)
            {
                Debug.WriteLine(exception);
                return null;
            }
        }
        public static DataTable GetDataWithParameters(string query, List<SqlParameter> parameters)
        {
            try
            {
                DBConnection dbconnection = new DBConnection();
                dbconnection.OpenConnection();
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, dbconnection.connection))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        parameters.ForEach(parameter => {
                            cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                        });
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    
                }
                dbconnection.CloseConnection();
                return dt;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return null;
            }
        }
    }
}
