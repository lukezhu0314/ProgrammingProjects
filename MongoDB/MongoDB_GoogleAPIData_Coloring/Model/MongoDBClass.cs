using System;
using System.Collections.Generic;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB_GoogleAPIData_Coloring.Model
{
    public class MongoDBClass
    {
        public static ListofRGB RGBGeneration() {
            var client = new MongoClient();
            var db = client.GetDatabase("PreliminaryTravelTime");
            var collection = db.GetCollection<TravelTime>("TravelTime(100x50)");

            var listOfRGB = new ListofRGB();
            listOfRGB.Values = new List<RGB>();

            var travelTimes = collection.Find(_ => true).ToListAsync().Result;
            foreach(var travelTime in travelTimes)
            {
                if (travelTime.Second == null)
                {
                    listOfRGB.Values.Add(new RGB{Red = 128, Green = 128, Blue = 128});
                }
                else
                {
                    int colorFraction;
                    if(travelTime.Second < (3600 * 3))
                    {
                        colorFraction = (int)travelTime.Second / (3600 * 3);
                        listOfRGB.Values.Add(new RGB{Red = 255 * (1 - colorFraction), Green = 255 * colorFraction, Blue = 255 * colorFraction});
                    }
                    else
                    {
                        listOfRGB.Values.Add(new RGB{Red = 20, Green = 255, Blue = 20});
                    } 
                }
            }
            return listOfRGB;
        }
        
        public static ListofRGB RandomRGBGeneration(int count)
        {
            var listofRGB = new ListofRGB();
            listofRGB.Values = new List<RGB>();
            Random rnd = new Random();
            /*
            for(int i = 0; i < count; i++) {
                listofRGB.Values.Add(new RGB{Red = rnd.Next(1,255), Green = rnd.Next(1,255), Blue = rnd.Next(1,255)});
            }
            */
            for(int i = 0; i < count; i++) {
                if(i < count / 2) {
                    listofRGB.Values.Add(new RGB{Red = 0, Green = 0, Blue = 0});
                } else {
                    listofRGB.Values.Add(new RGB{Red = 255, Green = 255, Blue = 255});
                }
            }
            return listofRGB;
        }
            
    }

    public class ListofRGB
    {
        public List<RGB> Values { get; set; }
    }

    public class RGB
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }   
    }

    public class TravelTime
    {
        public ObjectId _id {get; set; }
        public int? Second { get; set; }
    }
}