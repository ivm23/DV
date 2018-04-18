using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
    
using System.Threading.Tasks;

namespace Registration.Model
{
    public class LetterProperties
    {
        public string ExtendedProperty { get; set; } = string.Empty;

        public override string ToString()
        {
            return ExtendedProperty.ToString();
        }
    }
}
