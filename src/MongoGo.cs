using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace src
{
    class MongoGo
    {
        public MongoGo()
        {
            this.start();
        }
        public void start()
        {
            // // or use a connection string
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("person");
            var document = new BsonDocument
                {
                    { "name", "Кононов Петр" },
                    { "birth", new DateTime(1992,10,31,15,00,0,0) },
                    { "gender", "М" },
                    { "prof", "Студент"},
                    { "hobby", new BsonArray
                        {
                            "Футбол","Покер"
                        }}
                };
            // collection.InsertOne(document);

            // var document2 = collection.Find(new BsonDocument()).Sort("{name: 1}").ToList();
            // foreach (var p in document2)
            // {
            //      Console.WriteLine(p.ToString());
            // }

            var document3 = collection.Find(Builders<BsonDocument>.Filter.SizeGt("hobby", 0)).Sort("{name: 1}").ToList();
            Console.WriteLine("lol");
            foreach (var p in document3)
            {
                 Console.WriteLine(p.ToString());
            }
        }
    }
}