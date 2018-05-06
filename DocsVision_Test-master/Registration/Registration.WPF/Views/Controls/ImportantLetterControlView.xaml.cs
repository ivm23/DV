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


namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для ImportantLetterControlView.xaml
    /// </summary>
    public partial class ImportantLetterControlView : UserControl, ILetterPropertiesUIPlugin
    {

        private ViewModels.ImportantLetterControlViewModel importantLetterControlViewModel;
        private LetterView _letterView = new LetterView();

        public ImportantLetterControlView()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            fullContentLetterControl.OnLoad(serviceProvider);

            _letterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
            importantLetterControlViewModel = new ViewModels.ImportantLetterControlViewModel(_letterView);

            DataContext = importantLetterControlViewModel;

            importantLetterControlViewModel.InitializeImportanceDegreeControl();
        }


        public LetterView LetterView
        {
            set
            {
                fullContentLetterControl.LetterView = value;
                importantLetterControlViewModel.StringSelectedImportanceDegree = value.ExtendedData;
            }
            get
            {
                _letterView = fullContentLetterControl.LetterView;
                _letterView.ExtendedData = importantLetterControlViewModel.StringSelectedImportanceDegree;
                return _letterView;
            }
        }

        public bool ReadOnly
        {
            set
            {
                fullContentLetterControl.ReadOnly = value;
                importantLetterControlViewModel.ReadOnly = value;
            }
            get
            {
                return fullContentLetterControl.ReadOnly;
            }
        }
    }
}