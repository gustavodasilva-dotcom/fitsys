using Domain.Entities;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Domain.Abstractions
{
    public interface IPersonsRepository
    {
        Task<List<Person>> GetAll(int skip = 0, int limit = 50);
        Task<List<Person>> GetAll(Expression<Func<Person, bool>> expression, int skip = 0, int limit = 50);
        Task<Person> Get(Expression<Func<Person, bool>> expression);
        Task Save(Person entity);
        Task Update(Person entity);
        Task Delete(ObjectId Id);
    }
}
