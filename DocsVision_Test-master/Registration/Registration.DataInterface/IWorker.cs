using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataInterface
{
   public interface IWorker
    {
        string Name { get; set; }
        string Login { get; set; }
        string Password { get; set; }

        Guid Id { get; }

        string ToString();
    }
}
