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
    /// Interaction logic for LetterWithResponseTimeControl.xaml
    /// </summary>
    public partial class LetterWithResponseTimeControl : UserControl//, ILetterPropertiesUIPlugin
    {
        public LetterWithResponseTimeControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            LetterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
        }

        public LetterView LetterView { set; get; }
        public bool Enable { set; get; }


        public event EventHandler AddedReceiver;

        private bool _readOnly;
        public bool ReadOnly
        {
            set
            {
                _readOnly = value;
                Enable = !value;
            }
            get
            {
                return _readOnly;
            }
        }

        
    }
}
