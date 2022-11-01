using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{

    public interface IDBConnection
    {
        void OpenConnection();
        void CloseConnection();
        SqlConnection connection { get; set; }
    }
}
