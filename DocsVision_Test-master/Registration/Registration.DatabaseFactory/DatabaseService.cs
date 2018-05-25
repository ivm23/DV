using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Registration.DatabaseFactory
{
    public abstract class DatabaseService
    {
        public string connectionString;
        public abstract IDatabaseConnection CreateConnection();
      //  public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);
        public abstract IDatabaseCommand CreateStoredProcCommand(string procName, IDatabaseConnection connection);
        public abstract void AddParameterWithValue(string parameterName, object prameterValue, IDatabaseCommand command);
    }
}
