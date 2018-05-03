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
using Registration.WinForms;
using Registration.Model;


namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for FullContentLetterControl.xaml
    /// </summary>
    public partial class FullContentLetterControl : UserControl, ILetterPropertiesUIPlugin
    {
        public FullContentLetterControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            LetterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
        }

        private LetterView _letterView;

        public LetterView LetterView
        {
            set
            {
                _letterView = value;
                title.Text = value.Name;
                sender.Text = value.SenderName;
                message.Text = value.Text;
                date.Text = value.Date.ToString();
            }
            get
            {
                return _letterView;
            }
        }

        public event EventHandler AddedReceiver;

        private bool _readOnly;

        public bool ReadOnly
        {
            set
            {
                _readOnly = value;
                title.IsReadOnly = value;
                sender.IsReadOnly = value;
                message.IsReadOnly = value;
                date.IsReadOnly = value;

                workersEditorControl.ReadOnly = value;
            }
            get
            {
                return _readOnly;
            }
        }

    }
}
