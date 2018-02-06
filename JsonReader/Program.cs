using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace JsonReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonText = File.ReadAllText("PolygonRingData.json");
            var sponsors = JsonConvert.DeserializeObject<RingCollection>(jsonText);
        }
    }

    class RingCollection 
    {
        public List<List<double[]>> Rings { get; set; }
    }
}
