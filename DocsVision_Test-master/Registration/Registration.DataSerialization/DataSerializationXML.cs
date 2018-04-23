using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;


namespace Registration.DataSerialization
{
    public class DataSerializationXML : IDataSerializationService
    {
        public string SerializeData<T>(T data)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(writer, data);
                    return stringWriter.ToString();
                }
            }
        }

        public T DeserializeData<T>(string serializeData)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(serializeData))
            {
                using (var reader = XmlReader.Create(stringReader))
                {
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
        }

    }
}
