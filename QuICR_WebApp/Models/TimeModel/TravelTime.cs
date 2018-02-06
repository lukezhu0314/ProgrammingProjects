using MongoDB.Bson;

namespace QuICR_WebApp.Models.TimeModel
{
    public class TravelTime
    {
        public ObjectId _id { get; set; }
        public int? DnS_PscTime { get; set; }
        public int? DnS_Psc_CscTime { get; set; }
        public int? MS_CscTime { get; set; }
    }
}