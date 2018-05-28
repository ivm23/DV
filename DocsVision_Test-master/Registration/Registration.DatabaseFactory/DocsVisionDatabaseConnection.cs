using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Platform.ObjectManager;

namespace Registration.DatabaseFactory
{
    public class DocsVisionDatabaseConnection : DocsVisionDatabase, IDatabaseConnection
    {
        private readonly UserSession _userSession;

        public DocsVisionDatabaseConnection(UserSession userSession)
        {
            _userSession = userSession;
        }

        public void Dispose()
        {
            _userSession.Close();
        }
    }

}
