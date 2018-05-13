using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Registration.DatabaseFactory;

namespace Registration.Api
{
    public class DatabasesService : IDatabasesService
    {
        IDictionary<string, DatabaseService> _databaseServices = new Dictionary<string, DatabaseService>();

        const string ConnectionStringKey = "connectionString";

        private string GetConnectionString(string databaseName) => $"Data Source = MARINA-ПК\\SQLEXPRESS; Initial Catalog = {databaseName}; Integrated Security = True";

        public void InitializeDatabasesService()
        {
            ConfigurationService configurationService = ConfigurationServiceFactory.InitializeConfigurationService();

            foreach (ConnectionInfo connectionInfo in configurationService.GetConnections())
            {
                if (!_databaseServices.Keys.Contains(connectionInfo.ConnectionString))
                {
                    _databaseServices.Add(connectionInfo.ConnectionString, DatabaseFactory.DatabaseFactory.InitializeDatabase(GetConnectionString(connectionInfo.ConnectionString)));
                }
            }

        }

        public IDictionary<string, DatabaseService> GetDatabasesService()
        {
            return _databaseServices;
        }
    }

}