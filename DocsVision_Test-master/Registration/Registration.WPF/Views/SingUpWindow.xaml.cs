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
using System.Windows.Shapes;

namespace Registration.WPF.Views
{
    /// <summary>
    /// Interaction logic for SingUpWindow.xaml
    /// </summary>
    public partial class SingUpWindow : Window
    {
        public SingUpWindow(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException(); 

            InitializeComponent();
            DataContext = new ViewModels.SingUpViewModel(provider);
        }
    }
}
