using System;
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

namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for AuthorizationControl.xaml
    /// </summary>
    public partial class AuthorizationControl : UserControl
    {
        public AuthorizationControl()
        {
            InitializeComponent();
        }

        private void RetranslateToTextBox(string password)
        {
            passwordTextBox.Text = password;
        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RetranslateToTextBox(passwordBox.Password);
        }
    }
}
