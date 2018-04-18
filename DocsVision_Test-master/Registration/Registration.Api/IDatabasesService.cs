using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.DatabaseFactory;

namespace Registration.Api
{
    interface IDatabasesService
    {
        void InitializeDatabasesService();
        IDictionary<string, DatabaseService> GetDatabasesService();
    }
}
