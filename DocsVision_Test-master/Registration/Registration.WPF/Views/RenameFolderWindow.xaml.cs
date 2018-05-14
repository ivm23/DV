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
    /// Interaction logic for RenameFolderWindow.xaml
    /// </summary>
    public partial class RenameFolderWindow : Window
    {
        public RenameFolderWindow( IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            InitializeComponent();
            DataContext = new ViewModels.RenameFolderViewModel(provider);
            this.SizeToContent = SizeToContent.WidthAndHeight;
            txtNewName.SelectionStart = txtNewName.Text.Length;
        }

        private void ChangeDialogResult(bool dialogResult)
        {
            this.DialogResult = dialogResult;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            ChangeDialogResult(true);
        }
    }
}
