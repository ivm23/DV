using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DatabaseFactory
{
    public class DocsVisionDatabaseReader : DocsVisionDatabase, IDatabaseReader
    {
        public bool GetBool(string str)
        {
            return false;
        }
        public int GetInt(string str)
        {
            return 0;
        }
        public Guid GetGuid(string str)
        {
            return Guid.Empty;
        }

        public string GetString(string str)
        {
            return String.Empty;
        }

        public DateTime GetDateTime(string str)
        {
            return DateTime.MaxValue;
        }

        public bool Read()
        {
            return false;
        }
        public void Dispose()
        {
            
        }
    }
}
