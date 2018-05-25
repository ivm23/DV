using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Registration.DatabaseFactory
{
    public class SqlDatabaseConnection : MsSqlDatabase, IDatabaseConnection
    {
        private readonly SqlConnection _sqlConnection;

        public SqlDatabaseConnection(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        public SqlConnection SqlConnection
        {
            get
            {
                return _sqlConnection;
            }
        }
        
    }
}
