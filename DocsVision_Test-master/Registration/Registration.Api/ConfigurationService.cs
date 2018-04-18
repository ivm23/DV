using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Api
{
    public class ConnectionInfo
    {
        public string Name { get; set; }
        public string ConnectionString{ get; set; }
    }

    public interface ConfigurationService
    {
        IEnumerable<ConnectionInfo> GetConnections();
    }

}