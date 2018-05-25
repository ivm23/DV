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
using System.Windows.Shapes;

namespace Registration.WPF.Views
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow(IServiceProvider provider)
        {
            InitializeComponent();
            DataContext = new ViewModels.AuthorizationViewModel(provider);
        }

        private void AuthorizationWindowName_Closed(object sender, EventArgs e)
        {
            if (this.DialogResult != true)
                this.DialogResult = false;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
