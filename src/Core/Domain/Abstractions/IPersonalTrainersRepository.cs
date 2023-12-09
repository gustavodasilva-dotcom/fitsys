using System.Linq.Expressions;
using Domain.Entities;
using MongoDB.Bson;

namespace Domain.Abstractions;

public interface IPersonalTrainersRepository
{
    Task<List<PersonalTrainer>> GetAll(int skip = 0, int limit = 50);
    Task<List<PersonalTrainer>> GetAll(Expression<Func<PersonalTrainer, bool>> expression, int skip = 0, int limit = 50);
    Task<PersonalTrainer> Get(Expression<Func<PersonalTrainer, bool>> expression);
    Task Save(PersonalTrainer entity);
    Task Update(PersonalTrainer entity);
    Task Delete(ObjectId Id);
}