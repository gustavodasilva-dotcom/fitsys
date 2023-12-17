using System.Linq.Expressions;
using Domain.Entities;
using MongoDB.Bson;

namespace Domain.Abstractions;

public interface IEmployeesRepository
{
    Task<List<Employee>> GetAll(int skip = 0, int limit = 50);
    Task<List<Employee>> GetAll(Expression<Func<Employee, bool>> expression, int skip = 0, int limit = 50);
    Task<Employee> Get(Expression<Func<Employee, bool>> expression);
    Task Save(Employee entity);
    Task Update(Employee entity);
    Task Delete(ObjectId Id);
}