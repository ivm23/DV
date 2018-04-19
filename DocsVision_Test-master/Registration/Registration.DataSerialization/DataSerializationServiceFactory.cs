using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Registration.Api;

namespace Registration.DataSerialization
{
    public sealed class DataSerializationServiceFactory
    {
        private static DataSerializationServiceFactorySectionHandler sectionHandler = (DataSerializationServiceFactorySectionHandler)ConfigurationManager.GetSection("DataSerializeService");

        private DataSerializationServiceFactory() { }

        public static IDataSerializationService InitializeDataSerializationService()
        {
            if (sectionHandler != null)

                if (string.IsNullOrEmpty(sectionHandler.Name))
                    throw new Exception("Configuration name not defined in dataSerializationService section of App.config.");

            try
            {
                return (IDataSerializationService)Activator.CreateInstance(Type.GetType(sectionHandler.Name));
            }
            catch (Exception excep)
            {
                throw new Exception("Error instantiating dataSerializationService " + sectionHandler.Name + ". " + excep.Message);
            }

        }
    }
}
