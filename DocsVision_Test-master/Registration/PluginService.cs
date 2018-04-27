using System;
using System.Collections.Generic;
using Registration.Model;
using Registration.ClientInterface;
using Registration.WPF;

namespace Registration
{
    public class PluginService : IPluginService
    {
        private readonly IServiceProvider _serviceProvider;

        public PluginService(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException(nameof(provider));

            _serviceProvider = provider;
        }

        private IServiceProvider ServiceProvider => _serviceProvider;


        public IFolderPropertiesUIPlugin GetFolderPropetiesPlugin(FolderType selectedFolderType)
        {
            return GetPlugin<IFolderPropertiesUIPlugin>(selectedFolderType.Id, GetFolderPluginTypeName);
        }

        public ILetterPropertiesUIPlugin GetLetterPropetiesPlugin(LetterType selectedLetterType)
        {
            return GetPlugin<ILetterPropertiesUIPlugin>(selectedLetterType.Id, GetLetterPluginTypeName);
        }

        private T GetPlugin<T>(int typeId, Func<int, string> typeNameGetter)
        {
            if (null == typeNameGetter)
                throw new ArgumentNullException(nameof(typeNameGetter));
            
               return CreatePlugin<T>(typeNameGetter(typeId));
        }


        private T CreatePlugin<T>(string typeName)
        {
            var obj = new ImportanceDegree();//Type.GetType(typeName);
            return (T)Activator.CreateInstance(obj.GetType());
        }


        private string GetFolderPluginTypeName(int folderTypeId)
        {
            IClientRequests clientRequests = (IClientRequests)(ServiceProvider.GetService(typeof(IClientRequests)));

            return clientRequests.GetFolderType(folderTypeId).TypeClientUI;
        }

        private string GetLetterPluginTypeName(int letterTypeId)
        {
            IClientRequests clientRequests = (IClientRequests)(ServiceProvider.GetService(typeof(IClientRequests)));

            return clientRequests.GetLetterType(letterTypeId).TypeClientUI;
        }
    }
}
