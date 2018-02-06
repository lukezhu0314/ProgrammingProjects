using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB_ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("startup");
            var collection = database.GetCollection<BsonDocument>("employee");

            var document = new BsonDocument
            {
                { "name", "Alex" },
                { "Age", "20" },
                { "Salary", "12000" },
                { "position", "designer" },
            };
            collection.InsertOneAsync(document);
            Console.Read();
        }
    }
}
