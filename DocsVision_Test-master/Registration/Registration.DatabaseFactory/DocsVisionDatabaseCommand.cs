using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DatabaseFactory
{
    public class DocsVisionDatabaseCommand : DocsVisionDatabase, IDatabaseCommand
    {
        public void ExecuteNonQuery()
        {

        }

        public IDatabaseReader ExecuteReader()
        {
            return null;
        }
    }
}
