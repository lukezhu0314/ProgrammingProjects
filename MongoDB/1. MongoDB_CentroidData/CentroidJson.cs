using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MongoDB_CentroidData
{
    public class CentroidJson
    {
        public static List<latlong> CentroidJsonfunc()
        {
            var jsonText = File.ReadAllText("ListofCentroids(100x50)v2.json");
            var centroidCollection = JsonConvert.DeserializeObject<List<latlong>>(jsonText);
            return centroidCollection;
        }
   }

    public class CentroidCollection
    {
        public latlong centroidCollection { get; set; }
    }

    public class latlong
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}