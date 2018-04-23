using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataSerialization
{
    public interface IDataSerializationService
    {
        string SerializeData<T>(T data);
        T DeserializeData<T>(string serializeData);
    }
}
