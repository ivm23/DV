using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration
{
    public interface IPropertiesUIPlugin
    {
        void OnLoad(IServiceProvider serviceProvider);
    }
}
