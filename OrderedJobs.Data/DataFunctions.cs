using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace OrderedJobs.Data
{
    public class DataFunctions
    {
        private static MongoClient _mongoClient;
        private static IMongoDatabase _mongoDatabase;
        private static IMongoCollection<BsonDocument> _testsCollection;

        public DataFunctions()
        {
            var connectionString = "mongodb://localhost:27017";
            _mongoClient = new MongoClient(connectionString);
            _mongoDatabase = _mongoClient.GetDatabase("OrderedJobs");
            _testsCollection = GetTestsCollection();
        }

        private static IMongoCollection<BsonDocument> GetTestsCollection()
        {
            return _mongoDatabase.GetCollection<BsonDocument>("Tests");
        }

        public void AddTest(string test)
        {
            var testToAdd = new BsonDocument
            {
                {"Test", new BsonString(test)} 
            };
          _testsCollection.InsertOne(testToAdd);
        }

        public Task<List<string>> GetTests()
        {
            var testList = _testsCollection.FindAsync(new BsonDocument()).Result.ToList()
                .Select( document => document.ToString()).ToList();
            return Task.FromResult(testList);
        }

        public bool DeleteTests()
        {
            var result = _testsCollection.DeleteMany(new BsonDocumentFilterDefinition<BsonDocument>(new BsonDocument()));
            Console.WriteLine(result.DeletedCount.ToString());
            return result.IsAcknowledged;
        }
    }
}
