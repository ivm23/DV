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

namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for SearchFolderControl.xaml
    /// </summary>
    public partial class SearchFolderControl : UserControl//, IFolderPropertiesUIPlugin
    {
        public event EventHandler ChangedFolderTypePlugin;

        public SearchFolderControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            //  if (null == serviceProvider)
            //      throw new ArgumentNullException();

            //   _serviceProvider = serviceProvider;
            //   createFolderControl1.InitializeFolderTypes(ServiceProvider);
          //  CurrentFolerPropertiesPlugin = this;
        }

        public FolderType FolderType
        {
            set
            {
                // createFolderControl1.FolderType = value;
            }
            get
            {
                return null;// createFolderControl1.FolderType;
            }
        }

        private void createFolderControl_ChangedFolderType(object sender, EventArgs e)
        {
            // ChangedFolderTypePlugin(this, e);
        }

        public FolderProperties FolderProperties
        {
            set
            {
                //   _info = value;
            }
            get
            {
                //    _info.Properties.Clear();
                //    if (_info == null)
                //        _info = new global::Registration.Model.FolderProperties();
                //      _info.Name = createFolderControl1.NameF;

                return null;//_info;
            }
        }

        public IFolderPropertiesUIPlugin CurrentFolerPropertiesPlugin { get; set; }

    }
}
