using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registration.Model;
using System.Xml;

namespace Registration.WinForms.Controlers
{
    public partial class StandartLetterControl : UserControl //ILetterPropertiesUIPlugin
    {
        public StandartLetterControl()
        {
            InitializeComponent();
        }
        LetterProperties _info;

        public void OnLoad()
        {

        }

        public LetterProperties Properties
        {
            set
            {
                _info = value;
            }
            get
            {
                if (null == _info)
                {
                    _info = new LetterProperties();
                }
                return _info;
            }
        }
    }
}
