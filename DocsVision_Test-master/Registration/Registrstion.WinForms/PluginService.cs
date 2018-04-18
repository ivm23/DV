using System;
using System.Collections.Generic;
using Registration.Model;
using Registration.ClientInterface;

namespace Registration.WinForms
{
    public class PluginService : IPluginService
    {
        private readonly IDictionary<int, IFolderPropertiesUIPlugin> _existClientPlugin = new Dictionary<int, IFolderPropertiesUIPlugin>();
        private readonly IDictionary<Guid, object> _globalExistClientPlugin = new Dictionary<Guid, object>();
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
            if (null == selectedFolderType)
                throw new ArgumentNullException(nameof(selectedFolderType));

            IFolderPropertiesUIPlugin clientUIPlugin;

            if (!_existClientPlugin.TryGetValue(selectedFolderType.Id, out clientUIPlugin))
            {
                IClientRequests clientRequests = (IClientRequests)(ServiceProvider.GetService(typeof(IClientRequests)));

                string typeClientFolderPropertiesUI = clientRequests.GetFolderType(selectedFolderType.Id).TypeClientUI;
                clientUIPlugin = CreatePlugin<IFolderPropertiesUIPlugin>(typeClientFolderPropertiesUI);

                _existClientPlugin.Add(selectedFolderType.Id, clientUIPlugin);
            }
            return clientUIPlugin;
        }

        public ILetterPropertiesUIPlugin GetLetterPropetiesPlugin(LetterType selectedLetterType)
        {
            if (null == selectedLetterType)
                throw new ArgumentNullException(nameof(selectedLetterType));

            IClientRequests clientRequests = (IClientRequests)(ServiceProvider.GetService(typeof(IClientRequests)));
            string typeClientLetterPropertiesUI = clientRequests.GetLetterType(selectedLetterType.Id).TypeClientUI;
            return CreatePlugin<ILetterPropertiesUIPlugin>(typeClientLetterPropertiesUI);
        }

        private T CreatePlugin<T>(string typeName)
        {
            var obj = Type.GetType(typeName);
            return (T)Activator.CreateInstance(obj);
        }

        //public IFolderPropertiesUIPlugin GetFolderPropetiesPlugin(FolderType selectedFolderType)
        //{
        //    return GetPlugin<IFolderPropertiesUIPlugin>(Guid.Empty, GetFolderPluginTypeName);
        //}

        //private T GetPlugin<T>(Guid typeId, Func<Guid, string> typeNameGetter)
        //{
        //    if (typeId == Guid.Empty)
        //        throw new ArgumentOutOfRangeException(nameof(typeId));
        //    if (typeNameGetter == null)
        //        throw new ArgumentNullException(nameof(typeNameGetter));

        //    object clientUIPlugin;

        //    if (!_globalExistClientPlugin.TryGetValue(typeId, out clientUIPlugin))
        //    {
        //        clientUIPlugin = CreatePlugin<T>(typeNameGetter(typeId));
        //        _globalExistClientPlugin.Add(typeId, clientUIPlugin);
        //    }

        //    return (T)clientUIPlugin;

        //}

        //private string GetFolderPluginTypeName(Guid folderTypeId)
        //{
        //    IClientRequests clientRequests = (IClientRequests)(ServiceProvider.GetService(typeof(IClientRequests)));

        //    string typeClientFolderPropertiesUI = clientRequests.GetFolderType(folderTypeId).TypeClientUI;
        //}
    }
}
