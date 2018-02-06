using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace MongoDB_GoogleAPIData_Coloring.Model
{
    public class GetJson
    {
        public static List<RingCollection> GetJsonFunc(string filename)
        {
            var jsonText = File.ReadAllText(filename);
            var ringCollection = JsonConvert.DeserializeObject<List<RingCollection>>(jsonText);
            return ringCollection;
        }
    }

    public class RingCollection 
    {
        public List<List<double[]>> Rings { get; set; }
    }
}