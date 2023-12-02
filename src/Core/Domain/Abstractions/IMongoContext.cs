using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Abstractions
{
    public interface IMongoContext
    {
        IMongoCollection<Person> Persons { get; }
    }
}
