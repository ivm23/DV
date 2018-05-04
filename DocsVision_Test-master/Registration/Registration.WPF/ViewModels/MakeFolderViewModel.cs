﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Registration.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Registration.ClientInterface;
using Registration.WPF.ViewModels;
using Registration.WinForms;


namespace Registration.WPF.ViewModels
{
    class MakeFolderViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private Worker _worker;
        public MakeFolderViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;
        }
        private IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private void InitializeWorker()
        {
            _worker = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker;
        }

        public IFolderPropertiesUIPlugin FolderPlugin { get; set; }

        public void InitializeFolderPlugin()
        {
            InitializeClientRequests();
            InitializeWorker();

            FolderPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            FolderPlugin.OnLoad(ServiceProvider);
        }

    }
}
