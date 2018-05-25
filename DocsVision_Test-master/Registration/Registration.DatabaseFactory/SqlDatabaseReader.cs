using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Registration.DatabaseFactory
{
    public class SqlDatabaseReader : MsSqlDatabase, IDatabaseReader
    {
        private readonly IDataReader _reader;

        public SqlDatabaseReader(IDataReader reader)
        {
            _reader = reader;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public int GetInt(string str)
        {
            return _reader.GetInt32(_reader.GetOrdinal(str));
        }
        public string GetString(string str)
        {
            return _reader.GetString(_reader.GetOrdinal(str));
        }
        public bool GetBool(string str)
        {
            return _reader.GetBoolean(_reader.GetOrdinal(str));
        }
        public Guid GetGuid(string str)
        {
            return _reader.GetGuid(_reader.GetOrdinal(str));
        }

        public DateTime GetDateTime(string str)
        {
            return _reader.GetDateTime(_reader.GetOrdinal(str));
        }

        public bool Read()
        {
            return _reader.Read();
        }
    }
}
