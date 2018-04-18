﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataSerialization
{
    public interface IDataSerializationService<T>
    {
        string SerializeData(T data);
        T DeserializeData(string serializeData);
    }
}
