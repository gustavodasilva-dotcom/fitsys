using Domain.Abstractions;
using Domain.Entities;
using Humanizer;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure
{
    public class MongoContext : IMongoContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["MongoDB:ConnectionString"]);
            _database = _mongoClient.GetDatabase(_configuration["MongoDB:Database"]);
        }

        public IMongoCollection<Client> Clients
        {
            get
            {
                return _database.GetCollection<Client>(nameof(Client).Pluralize());
            }
        }

        public IMongoCollection<Employee> Employees
        {
            get
            {
                return _database.GetCollection<Employee>(nameof(Employee).Pluralize());
            }
        }

        public IMongoCollection<Constant> Constants
        {
            get
            {
                return _database.GetCollection<Constant>(nameof(Constant).Pluralize());
            }
        }

        public IMongoCollection<Exercise> Exercises
        {
            get
            {
                return _database.GetCollection<Exercise>(nameof(Exercise).Pluralize());
            }
        }
    }
}
