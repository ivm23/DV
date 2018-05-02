using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.WPF.Models
{
    public class LettersTypes
    {
        public IEnumerable<string> Names { set; get; }
        public LetterType FullLetterType { set; get; }
    }
}
