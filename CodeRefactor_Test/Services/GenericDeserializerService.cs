using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CodeRefactor_Test.Services
{
    public class GenericDeserializerService<T>
    {
        public List<T> Deserialize(string filename)
        {
            var jsonText = File.ReadAllText(filename);
            var deserializedObject = JsonConvert.DeserializeObject<List<T>>(jsonText);
            return deserializedObject;
        }
    }
}