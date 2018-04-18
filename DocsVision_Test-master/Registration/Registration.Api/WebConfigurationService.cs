using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Registration.Api
{
    public class WebConfigurationService : ConfigurationService
    {
        public IEnumerable<ConnectionInfo> GetConnections()
        {
            var connectionsInfo = new List<ConnectionInfo>();
            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                connectionsInfo.Add(new ConnectionInfo()
                {
                    Name = key,
                    ConnectionString = ConfigurationManager.AppSettings[key]
                });
            }

            return connectionsInfo;
        }
    }
}