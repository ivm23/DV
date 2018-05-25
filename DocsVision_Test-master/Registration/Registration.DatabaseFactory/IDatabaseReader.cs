using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DatabaseFactory
{
    public interface IDatabaseReader : IDisposable
    {
        int GetInt(string str);
        string GetString(string str);
        Guid GetGuid(string str);
        bool GetBool(string str);

        DateTime GetDateTime(string str);
        bool Read();
    }
}
