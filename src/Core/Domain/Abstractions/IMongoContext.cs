using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Abstractions
{
    public interface IMongoContext
    {
        IMongoCollection<Client> Clients { get; }
        IMongoCollection<Employee> Employees { get; }
        IMongoCollection<Constant> Constants { get; }
        IMongoCollection<Exercise> Exercises { get; }
    }
}
