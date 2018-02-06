using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using QuICR_WebApp.Models.GraphicsModel;

namespace QuICR_WebApp.Servives
{
    public class JsonDeserializerService
    {
        public List<PolygonGeometry> Deserializer(string filename)
        {
            var jsonText = File.ReadAllText(filename);
            var polygonGeometry = JsonConvert.DeserializeObject<List<PolygonGeometry>>(jsonText);
            return polygonGeometry;
        }
    }
}