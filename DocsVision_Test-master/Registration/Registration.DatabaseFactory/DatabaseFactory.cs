using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace Registration.DatabaseFactory
{
    public sealed class DatabaseFactory
    {
        public static DatabaseFactorySectionHandler sectionHandler = (DatabaseFactorySectionHandler)ConfigurationManager.GetSection("DatabaseFactoryConfiguration");
        private DatabaseFactory() { }
        public static DatabaseService InitializeDatabase(string connectionString)
        {
            if (sectionHandler != null) 

            if (string.IsNullOrEmpty(sectionHandler.Name))
            {
                throw new Exception("Database name not defined in DatabaseFactoryConfiguration section of web.config.");
            }
            try
            {         
                Type database = Type.GetType(sectionHandler.Name);
                ConstructorInfo constructor = database.GetConstructor(new Type[] { });
             
                DatabaseService createdObject = (DatabaseService)constructor.Invoke(null);
                createdObject.connectionString = connectionString;
                return createdObject;
            }
            catch (Exception excep)
            {
                throw new Exception("Error instantiating database " + sectionHandler.Name + ". " + excep.Message);
            }
        }
    }
}
