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
        private readonly Guid _idUserSession;
        private readonly SessionManager _manager;
        public DocsVisionDatabaseConnection(SessionManager manager, Guid idUserSession)
        {
            _manager = manager;
            _idUserSession = idUserSession;
        }

        public void Dispose()
        {
            _manager.CloseSession(_idUserSession);
        }
    }

}
