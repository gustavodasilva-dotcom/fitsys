using System.Linq.Expressions;
using Domain.Abstractions;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public sealed class PersonalTrainersRepository(IMongoContext mongoContext) : IPersonalTrainersRepository
{
    private readonly IMongoContext _mongoContext = mongoContext;
    
    public async Task<List<PersonalTrainer>> GetAll(int skip = 0, int limit = 50)
    {
        return await _mongoContext.PersonalTrainers.Find(u => true).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<List<PersonalTrainer>> GetAll(Expression<Func<PersonalTrainer, bool>> expression, int skip = 0, int limit = 50)
    {
        return await _mongoContext.PersonalTrainers.Find(expression).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<PersonalTrainer> Get(Expression<Func<PersonalTrainer, bool>> expression)
    {
        return await _mongoContext.PersonalTrainers.FindSync(expression).FirstOrDefaultAsync();
    }

    public async Task Save(PersonalTrainer entity)
    {
        await _mongoContext.PersonalTrainers.InsertOneAsync(entity);
    }

    public async Task Update(PersonalTrainer entity)
    {
        await _mongoContext.PersonalTrainers.ReplaceOneAsync(u => u.id.Equals(entity.id), entity);
    }

    public async Task Delete(ObjectId Id)
    {
        await _mongoContext.PersonalTrainers.DeleteOneAsync(u => u.id.Equals(Id));
    }
}