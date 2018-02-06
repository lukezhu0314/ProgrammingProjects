using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace GoogleAPI_C_
{
    class Program
    {
        static void Main(string[] args)
        {
            Parent parsedResult = new Parent();
            MapsAPICall(ref parsedResult);
            Console.WriteLine(parsedResult.rows[0].elements[0].duration.value);
            Console.WriteLine("Hello World!");
        }

        private static void MapsAPICall(ref Parent parsedResult)
        {
        //Pass request to google api with orgin and destination details
            HttpWebRequest request =
                WebRequest.CreateHttp("http://maps.googleapis.com/maps/api/distancematrix/json?origins="
                + "51.123959,3.326682" + "&destinations=" + "51.158089" + "," + "4.145267" 
                + "&mode=Car&language=us-en&sensor=false");
                
            
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

    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public Element[] elements { get; set; }
    }

    public class Parent
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
    }
}
