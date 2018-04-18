using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;

namespace Registration.Api
{
    public sealed class ConfigurationServiceFactory
    {

        private static ConfigurationServiceFactorySectionHandle sectionHandler = (ConfigurationServiceFactorySectionHandle)ConfigurationManager.GetSection("ConfigurationService");

        private ConfigurationServiceFactory() { }

        public static ConfigurationService InitializeConfigurationService()
        {
            if (null != sectionHandler)

            if (string.IsNullOrEmpty(sectionHandler.Name))
                throw new Exception("Configuration name not defined in ConfigurationService section of web.config.");

            try
            {            
                return (ConfigurationService)Activator.CreateInstance(Type.GetType(sectionHandler.Name));
            }
            catch (Exception excep)
            {
                throw new Exception("Error instantiating configurationService " + sectionHandler.Name + ". " + excep.Message);
            }

        }
    }
}