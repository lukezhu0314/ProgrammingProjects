using CodeRefactor_Test.Models.DistanceModel;
using CodeRefactor_Test.Models.TimeModel;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CodeRefactor_Test.Models.ProbabilityModel;
using CodeRefactor_Test.Data;
using CodeRefactor_Test.Models.HospitalModel;
using GeoCoordinatePortable;
using System;

namespace CodeRefactor_Test.Services
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

        public async void GoogleCall()
        {
            var centroidDeserializer = new GenericDeserializerService<Centroid>();
            List<Centroid> centroids = centroidDeserializer.Deserialize("listofCentroids(100x50).json");

            var hospitalTimeDeserializer = new GenericDeserializerService<HospitalTravelTime>();
            List<HospitalTravelTime> hospitalTimes = hospitalTimeDeserializer.Deserialize("HospitalTimes.json");

            List<Hospital> AlbertaHospitals = AlbertaHospitalData.AlbertaHospitals;
            var timeService = new TimeService(AlbertaHospitals);
            
            foreach(var centroid in centroids)
            {
                List<string> closestHospitals = timeService.FindClosestHospitals(centroid);
                string pscName = closestHospitals[0];

                //Perform the Google API call
                ResponseDistanceMatrix parsedResult = await timeService.GetTravelTimes(closestHospitals, new GeoCoordinate(centroid.Lat, centroid.Long));

                InsertDocument(parsedResult, pscName, hospitalTimes);       
            }
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

        public void UpdateDocument()
        {
            var centroidDeserializer = new GenericDeserializerService<Centroid>();
            List<Centroid> centroids = centroidDeserializer.Deserialize("listofCentroids(100x50).json");
            int count = centroids.Count;

            var hospitalTimeDeserializer = new GenericDeserializerService<HospitalTravelTime>();
            List<HospitalTravelTime> hospitalTimes = hospitalTimeDeserializer.Deserialize("HospitalTimes.json");

            List<Hospital> AlbertaHospitals = AlbertaHospitalData.AlbertaHospitals;
            var timeService = new TimeService(AlbertaHospitals);
            
            List<TravelTime> travelTimes = ReadData();
            for(int i = 0; i < count; i++)
            {
                List<string> closestHospitals = timeService.FindClosestHospitals(centroids[i]);
                string pscName = closestHospitals[0];
                
                if(pscName == "St Mary's Hospital, AB" || pscName == "Queen Elizabeth II Hospital, AB")
                {
                    ResponseDistanceMatrix parsedResult = timeService.GetTravelTimes(closestHospitals, new GeoCoordinate(centroids[i].Lat, centroids[i].Long)).Result;

                    if(parsedResult.Rows[0].Elements[0].Duration != null)
                    {
                        int Updated_DnS_PscTime = parsedResult.Rows[0].Elements[0].Duration.Value;
                        int Updated_DnS_Psc_CscTime = PscCscTravelTime(pscName, hospitalTimes);
                        int Updated_MS_CscTime = parsedResult.Rows[0].Elements[1].Duration.Value;
                        
                        var id = travelTimes[i]._id;
                        collection.FindOneAndUpdate(
                            b => b._id == id, 
                            Builders<TravelTime>.Update.Set(b => b.DnS_Psc_CscTime, Updated_DnS_Psc_CscTime).
                                                        Set(b => b.DnS_PscTime, Updated_DnS_PscTime).
                                                        Set(b => b.MS_CscTime, Updated_MS_CscTime)
                                                        );
                    }
                    
                    
                }
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