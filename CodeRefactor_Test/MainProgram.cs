using System;
using System.Collections.Generic;
using System.IO;
using CodeRefactor_Test.Data;
using CodeRefactor_Test.Models.GraphicsModel;
using CodeRefactor_Test.Models.HospitalModel;
using CodeRefactor_Test.Models.ProbabilityModel;
using CodeRefactor_Test.Models.TimeModel;
using CodeRefactor_Test.Services;
using GeoCoordinatePortable;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CodeRefactor_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var AlbertaHospitals = AlbertaHospitalData.AlbertaHospitals;
            //Splitting up the CSCs from the PSCs
            var timeService = new TimeService(AlbertaHospitals);
            
            var jsonText = File.ReadAllText("ListofCentroids(100x50).json");
            var centroids = JsonConvert.DeserializeObject<List<Centroid>>(jsonText);
            
            //This part deserializes the hospitalTimes.json file, does google calls, and store all the time into the database
            
            var databaseService = new DatabaseService();
            var hospitalTimes = databaseService.Deserializer("HospitalTimes.json");

            databaseService.UpdateDocument();

            //for(int i = 0; i  centroids.Count / 2; i++) 
            for(int i = centroids.Count / 2; i < centroids.Count; i++)
            {
                List<string> closestHospitals = timeService.FindClosestHospitals(centroids[i]);
                string pscName = closestHospitals[0];
                
                //var travelTimes = timeService.GetTravelTimes(closestHospitals, new GeoCoordinate(centroids[i].Lat, centroids[i].Long)).Result;

                /*
                foreach(var travelTime in travelTimes)
                {
                    databaseService.InsertDocument(travelTime.Value, pscName, hospitalTimes);
                }
                */
            }

            /*
            foreach(var centroid in centroids) 
            {
                var closestHospitals = timeService.GenerateResponseFeature(centroid);
                var pscName = closestHospitals[0];
                var travelTimes = timeService.GetTravelTimes(closestHospitals, new GeoCoordinate(centroid.Lat, centroid.Long)).Result;

                foreach(var travelTime in travelTimes)
                {
                    databaseService.InsertDocument(travelTime.Value, pscName, hospitalTimes);
                }
            }
            */
            
            
            //This part aims to call the GetProbability Function
            /*
            var databaseService = new DatabaseService();
            var travelTimes = databaseService.ReadData();
            var fillColors = new List<Color>();
            //var responseFeatures = new List<ResponseFeature>();
            foreach(var travelTime in travelTimes)
            {
                var probService = new ProbabilityService();
                var responseFeature = probService.GetProbability(travelTime);
                var fillColor = StyleService.DefaultFilledStyleFromProbability(responseFeature);
                fillColors.Add(fillColor);
                //responseFeatures.Add(probService.GetProbability(travelTime));
            }
            */
        }
    }
}
