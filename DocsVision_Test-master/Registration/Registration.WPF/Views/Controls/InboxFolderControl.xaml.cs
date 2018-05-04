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
using Registration.Model;
using Registration.ClientInterface;

namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for InboxFolderControl.xaml
    /// </summary>
    public partial class InboxFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        public InboxFolderControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

           CurrentFolderPropertiesPlugin = this;
        }

        public FolderType FolderType
        {
            set
            {
            }
            get
            {
                return null;
            }
        }

        public IEnumerable<FolderType> FoldersTypes { get; set; }

        public FolderProperties FolderProperties
        {
            set
            {
            }
            get
            {
                return null;
            }
        }

        public string FolderName { set; get; }
        public IFolderPropertiesUIPlugin CurrentFolderPropertiesPlugin { get; set; }

    }
}
