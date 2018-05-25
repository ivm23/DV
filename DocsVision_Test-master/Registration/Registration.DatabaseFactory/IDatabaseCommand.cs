using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DatabaseFactory
{
    public interface IDatabaseCommand 
    {
        void ExecuteNonQuery();
        IDatabaseReader ExecuteReader();
    }
}
