using System;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_ConsoleApp_Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            BookContext ctx = new BookContext();
            /*
            for(var i = 0; i < 50; i++)
            {
                Book book = new Book
                {
                    Title = "MongoDB for C# Developers Edition 2",
                    ISBN = "1232efe890",
                    Publisher = "DevelopMentor Group" + i
                };
                ctx.Books.InsertOne(book);
            }
            */
            
            var client = new MongoClient();
            var db = client.GetDatabase("bookStore");
            var collection = db.GetCollection<Book>("Book");
            
            var bookTitle = "MongoDB for C# Developers Edition 2";
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var book = collection
                .Find(b => b.Title == bookTitle)
                .SortBy(b => b.Publisher)
                .ToListAsync()
                .Result;

            //var book = ctx.Books.Find(b => b.Title == bookTitle).ToListAsync().Result;
            //ctx.Books.InsertOneAsync(book);
            Console.WriteLine(stopWatch.ElapsedMilliseconds);
            foreach(var Book in book)
            {
                Console.WriteLine(Book.Publisher);
            }
            Console.Read();
        }
    }

    public class Book
    {
        public ObjectId Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        [BsonIgnoreIfNull]
        public int? PageCount { get; set; }
    }

    class BookContext
    {
        private IMongoDatabase db;
        public BookContext()
        {
            //This is being connected to the local server with a default port
            MongoClient client = new MongoClient();
            //var server = client.GetServer();
            //we now create a database with a name BookStore
            this.db = client.GetDatabase("bookStore");
            //create a collection named 'Book' of type Book
            //var collection = db.GetCollection<Book>("Book");
        }
        public IMongoCollection<Book> Books
        {
            get
            {
                return db.GetCollection<Book>("Book");
            }
        }
    }
}
