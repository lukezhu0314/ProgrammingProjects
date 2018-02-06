using System.Collections.Generic;
using System.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using QuICR_WebApp.Models.DistanceModel;
using QuICR_WebApp.Models.TimeModel;

namespace QuICR_WebApp.Servives
{
    public class DatabaseService
    {
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<TravelTime> collection;
        public DatabaseService() 
        {
            client = new MongoClient();
            db = client.GetDatabase("TravelTime");
            collection = db.GetCollection<TravelTime>("TravelTime(100x50)");
        }

        public void InsertDocument(ResponseDistanceMatrix parsedResult, string pscName, List<HospitalTravelTime> hospitalTimes)
        {
            if(parsedResult.Rows[0].Elements[0].Duration == null || parsedResult.Rows[0].Elements[1].Duration == null)
            {
                //travelTime.Add(new TravelTime{Second = null});
                collection.InsertOne(new TravelTime{
                    DnS_PscTime = null,
                    DnS_Psc_CscTime = null,
                    MS_CscTime = null
                });
            } 
            else 
            {
                //travelTime.Add(new TravelTime{Second = parsedResult.rows[0].elements[0].duration.value});
                collection.InsertOne(new TravelTime{
                    DnS_PscTime = parsedResult.Rows[0].Elements[0].Duration.Value,
                    DnS_Psc_CscTime = PscCscTravelTime(pscName, hospitalTimes),
                    MS_CscTime = parsedResult.Rows[0].Elements[1].Duration.Value
                });
            }
        }

        public List<TravelTime> ReadData()
        {
            var travelTime = collection.Find(_ => true).ToListAsync().Result;
            return travelTime;
        }
        
        public int PscCscTravelTime(string pscName, List<HospitalTravelTime> hospitalTimes)
        {
            List<HospitalTravelTime> test = hospitalTimes.FindAll(h => h.HospitalOrigin == pscName);
            int pscToCsc = 99999;
            foreach(HospitalTravelTime tt in test)
            {
                if (tt.TravelTime < pscToCsc)
                    pscToCsc = tt.TravelTime;
            }
            return pscToCsc;
        }
        

        public List<HospitalTravelTime> Deserializer(string filename)
        {
            var jsonText = File.ReadAllText(filename);
            var hospitalTravelTimes = JsonConvert.DeserializeObject<List<HospitalTravelTime>>(jsonText);
            return hospitalTravelTimes;
        }
    }
}