using System.Linq.Expressions;
using Domain.Entities;
using MongoDB.Bson;

namespace Domain.Abstractions;

public interface IExercisesRepository
{
    Task<List<Exercise>> GetAll(int skip = 0, int limit = 50);
    Task<List<Exercise>> GetAll(Expression<Func<Exercise, bool>> expression, int skip = 0, int limit = 50);
    Task<Exercise> Get(Expression<Func<Exercise, bool>> expression);
    Task Save(Exercise entity);
    Task Update(Exercise entity);
    Task Delete(ObjectId Id);
}