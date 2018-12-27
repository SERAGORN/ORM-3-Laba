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
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("person");

            //Задание №1

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
            collection.InsertOne(document);

            //Задание №2

            var document2 = collection.Find(new BsonDocument()).Sort("{name: 1}").ToList();
            foreach (var p in document2)
            {
                 Console.WriteLine(p.ToString());
            }

            //Задание №3

            var document3 = collection.Find(new BsonDocument("hobby", new BsonDocument("$exists", 1))).Sort("{name: 1}").ToList();
            foreach (var p in document3)
            {
                 Console.WriteLine(p.ToString());
            }
            
            //Задание №4

            var document4 = collection.Find(Builders<BsonDocument>.Filter.Gte("name", "К")).ToList();
            foreach (var p in document4)
            {
                 Console.WriteLine(p.ToString());
            }
            
            //Задание №5
            
            var document5 = collection.Find(
                new BsonDocument(
                    "$and", new BsonArray{
                        new BsonDocument(
                        "$and", new BsonArray{
                            new BsonDocument("birth", new BsonDocument("$lte", new DateTime(1992,12,27,0,00,0,0))),
                            new BsonDocument("birth", new BsonDocument("$gte", new DateTime(1968,12,27,0,00,0,0)))
                        }
                        ),
                        new BsonDocument("hobby", new BsonDocument("$exists", 1))
                    }
                )
            ).Sort("{birth: 1}").ToList();
            foreach (var p in document5)
            {
                 Console.WriteLine(p.ToString());
            }
            
            //Задание №6
            
            var document6 = collection.UpdateOne(new BsonDocument("name", "Винокуров Виктор"), new BsonDocument("$set", new BsonDocument("prof", "Программист")));
            
            //Задание №7
            
            var document7 = collection.Find(
                new BsonDocument(
                    "hobby", new BsonDocument (
                        "$elemMatch", new BsonDocument (
                            "$in", new BsonArray().Add("Музыка")
                        )
                    )
                )
            ).Sort("{name: 1}").ToList();
            foreach (var p in document7)
            {
                 Console.WriteLine(p.ToString());
            }

        }
    }
}