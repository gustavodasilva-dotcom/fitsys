using Domain.Abstractions;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public sealed class PersonsRepository(IMongoContext mongoContext) : IPersonsRepository
    {
        private readonly IMongoContext _mongoContext = mongoContext;

        public async Task<List<Person>> GetAll(int skip = 0, int limit = 50)
        {
            return await _mongoContext.Persons.Find(u => true).Skip(skip).Limit(limit).ToListAsync();
        }

        public async Task<List<Person>> GetAll(Expression<Func<Person, bool>> expression, int skip = 0, int limit = 50)
        {
            return await _mongoContext.Persons.Find(expression).Skip(skip).Limit(limit).ToListAsync();
        }

        public async Task<Person> Get(Expression<Func<Person, bool>> expression)
        {
            return await _mongoContext.Persons.FindSync(expression).FirstOrDefaultAsync();
        }

        public async Task Save(Person entity)
        {
            await _mongoContext.Persons.InsertOneAsync(entity);
        }

        public async Task Update(Person entity)
        {
            await _mongoContext.Persons.ReplaceOneAsync(u => u.Id.Equals(entity.Id), entity);
        }

        public async Task Delete(ObjectId Id)
        {
            await _mongoContext.Persons.DeleteOneAsync(u => u.Id.Equals(Id));
        }
    }
}
