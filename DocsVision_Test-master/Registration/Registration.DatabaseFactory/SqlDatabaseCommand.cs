using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Registration.DatabaseFactory
{
    public class SqlDatabaseCommand : IDatabaseCommand
    {
        private readonly IDbCommand _command;

        public SqlDatabaseCommand(IDbCommand command)
        {
            _command = command;
        }

        public void ExecuteNonQuery()
        {
            _command.ExecuteNonQuery();
        }

        public IDatabaseReader ExecuteReader()
        {
            return (new SqlDatabaseReader(_command.ExecuteReader()));
        }
        public IDbCommand SqlCommand
        {
            get { return _command;  }
        }
    }
}
