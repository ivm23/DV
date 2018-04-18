using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration.WinForms.Controlers
{
    public partial class ButtonCreateCancelControl : UserControl
    {
        public delegate void MyEventCreateFolder();
        public static MyEventCreateFolder EventHandlerCreateFolder;

        public ButtonCreateCancelControl()
        {
            InitializeComponent();
        }


        public Button CreateB
        {
            get
            {
                return createB;
            }
            set
            {
                createB = value;
            }
        }
    }
}
