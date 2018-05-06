﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Registration.Model;
using Registration.ClientInterface;


namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for FullContentLetterControl.xaml
    /// </summary>
    public partial class StandartLetterControl : UserControl, ILetterPropertiesUIPlugin
    {
        private ViewModels.StandartLetterControlViewModel standartLetterControlViewModel;
        private LetterView _letterView = new LetterView();

        public StandartLetterControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _letterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
            standartLetterControlViewModel = new ViewModels.StandartLetterControlViewModel(_letterView);
            DataContext = standartLetterControlViewModel;

            workersEditorControl.DataContext = new ViewModels.WorkersEditorControlViewModel(((IClientRequests)serviceProvider.GetService(typeof(IClientRequests))).GetAllWorkers());
            workersEditorControl.InitializeWorkersEditorControl(((IClientRequests)serviceProvider.GetService(typeof(IClientRequests))).GetAllWorkers());
        }

        public bool ReadOnly
        {
            set
            {
                standartLetterControlViewModel.ReadOnly = value;
                workersEditorControl.ReadOnly = value;
            }
            get
            {
                return standartLetterControlViewModel.ReadOnly;
            }
        }

        public LetterView LetterView
        {
            set
            {
                _letterView = value;
                standartLetterControlViewModel.Title = value.Name;
                standartLetterControlViewModel.Text = value.Text;
                standartLetterControlViewModel.Date = value.Date;
                standartLetterControlViewModel.SenderName = value.SenderName;
                workersEditorControl.NamesWorkers = value.ReceiversName;        
            }
            get
            {
                var letter = new LetterView() {
                    Name = standartLetterControlViewModel.Title,
                    Text = standartLetterControlViewModel.Text,
                    Date = standartLetterControlViewModel.Date,
                    SenderName = standartLetterControlViewModel.SenderName
                };
                    letter.ReceiversName.AddRange(workersEditorControl.NamesWorkers);
                return letter;
            }
        }
        
      
    }
}
