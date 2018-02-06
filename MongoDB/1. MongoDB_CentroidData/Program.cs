using System;
using System.Collections.Generic;
using GoogleClass;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace MongoDB_CentroidData
{
    class Program
    {
        static void Main(string[] args)
        {
            var centroidList = CentroidJson.CentroidJsonfunc();
            
            // var client = new MongoClient();
            // var db = client.GetDatabase("preliminaryTestingJson");
            // var collection = db.GetCollection<BsonDocument>("selfcreatedJSON");
            

            //Storing the time calls from googleAPI into database
            var newClient = new MongoClient();
            var newdb = newClient.GetDatabase("PreliminaryTravelTime");
            var newCollection = newdb.GetCollection<TravelTime>("TravelTime(100x50)");

            var travelTime = new List<TravelTime>();
            Parent parsedResult = new Parent();
            
            //extracting centroid data out of the database
            // var centroids = collection.Find(_ => true).ToListAsync().Result[0]["centroid"].AsBsonArray;
            // var centroidList = new List<CentroidData>();
            // var travelTime = new List<TravelTime>();

            // Parent parsedResult = new Parent();

            /*
            foreach(var centroid in centroidList)
            {
                double[] geocoord = new double[] {centroid.Lat, centroid.Long};
                MapsAPICall(ref parsedResult, geocoord);
                if(parsedResult.rows[0].elements[0].duration == null){
                    travelTime.Add(new TravelTime{Second = null});
                    newCollection.InsertOneAsync(new TravelTime{Second = null});
                } else {
                    travelTime.Add(new TravelTime{Second = parsedResult.rows[0].elements[0].duration.value});
                    newCollection.InsertOneAsync(new TravelTime{Second = parsedResult.rows[0].elements[0].duration.value});
                }
            } 
            */
            
            // foreach(var point in centroids){
            //     double x = (double)point[0];
            //     double y = (double)point[1];
            //     var geoCoord = GeoCoordinatesTransformation(x, y);

            //     MapsAPICall(ref parsedResult, geoCoord);
            //     //Console.WriteLine(parsedResult.rows[0].elements[0].duration.value);
            //     if(parsedResult.rows[0].elements[0].duration == null){
            //         travelTime.Add(new TravelTime{Second = null});
            //         newCollection.InsertOneAsync(new TravelTime{Second = null});
            //     } else {
            //         travelTime.Add(new TravelTime{Second = parsedResult.rows[0].elements[0].duration.value});
            //         newCollection.InsertOneAsync(new TravelTime{Second = parsedResult.rows[0].elements[0].duration.value});
            //     }
            //     centroidList.Add(new CentroidData{latitude = y, longitude = x});
            // }

            // var client2 = new MongoClient();
            // var db2 = client2.GetDatabase("PreliminaryTravelTime");
            // var collection2 = db2.GetCollection<TravelTime>("TravelTime");

            // var travelTime2 = collection2.Find(_ => true).ToListAsync().Result;
            
        }

       private static double[] GeoCoordinatesTransformation(double x, double y){
            double num3 = x / 6378137.0;
            double num4 = num3 * 57.295779513082323;
            double num5 = Math.Floor(((num4 + 180.0) / 360.0));
            double num6 = num4 - (num5 * 360.0);
            double num7 = 1.5707963267948966 - (2.0 * Math.Atan(Math.Exp((-1.0 * y) / 6378137.0)));
            x = num6;
            y = num7 * 57.295779513082323;
            double[] geoCoord = {y, x};
            return geoCoord;
       }

       private static void MapsAPICall(ref Parent parsedResult, double[] geoCoord)
        {
        //Pass request to google api with orgin and destination details
            HttpWebRequest request =
                WebRequest.CreateHttp("https://maps.googleapis.com/maps/api/distancematrix/json?origins="
                + geoCoord[0] + "," + geoCoord[1] + "&destinations=" + "51.036155,-114.191921" 
                + "&mode=Car&language=us-en&sensor=false&key=AIzaSyAMhRYs7DX9NENrnYMAfd8PfQJ3F4EY7eY");
                
            
            //Task<WebResponse> getWebResponseTask = request.GetResponseAsync();
            //HttpWebResponse response = await (HttpWebResponse)getWebResponseTask;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(result))
                {
                    parsedResult = JsonConvert.DeserializeObject<Parent>(result);
                }
            }
        }
    }

    public class CentroidData
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    
    public class TravelTime
    {
        public ObjectId _id {get; set; }
        public int? Second { get; set; }
    }
}
