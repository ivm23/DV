using Registration.DataInterface;
using Registration.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Registration.Logger;
using System.Configuration;
using Registration.DatabaseFactory;



namespace Registration.Api.Controllers
{
    public class DataBaseController : ApiController
    {
        [HttpGet]
        [Route("api/database")]
        public IEnumerable<string> GetConnectionStrings()
        {
            ConfigurationService configurationService = ConfigurationServiceFactory.InitializeConfigurationService();

            var allConnectionsStrings = new List<string>();
            foreach (ConnectionInfo connectionInfo in configurationService.GetConnections())
            {
                if (connectionInfo.Name.Contains("connectionString"))
                {
                    allConnectionsStrings.Add(connectionInfo.ConnectionString);
                }
            }
            return allConnectionsStrings;
        }
    }
}