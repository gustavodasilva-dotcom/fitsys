using System.Linq.Expressions;
using Domain.Abstractions;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public sealed class ExercisesRepository(IMongoContext mongoContext) : IExercisesRepository
{
    private readonly IMongoContext _mongoContext = mongoContext;

    public async Task<List<Exercise>> GetAll(int skip = 0, int limit = 50)
    {
        return await _mongoContext.Exercises.Find(u => true).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<List<Exercise>> GetAll(Expression<Func<Exercise, bool>> expression, int skip = 0, int limit = 50)
    {
        return await _mongoContext.Exercises.Find(expression).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<Exercise> Get(Expression<Func<Exercise, bool>> expression)
    {
        return await _mongoContext.Exercises.FindSync(expression).FirstOrDefaultAsync();
    }

    public async Task Save(Exercise entity)
    {
        await _mongoContext.Exercises.InsertOneAsync(entity);
    }

    public async Task Update(Exercise entity)
    {
        await _mongoContext.Exercises.ReplaceOneAsync(u => u.id.Equals(entity.id), entity);
    }

    public async Task Delete(ObjectId Id)
    {
        await _mongoContext.Exercises.DeleteOneAsync(u => u.id.Equals(Id));
    }
}