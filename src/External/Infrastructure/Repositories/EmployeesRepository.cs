using System.Linq.Expressions;
using Domain.Abstractions;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public sealed class EmployeesRepository(IMongoContext mongoContext) : IEmployeesRepository
{
    private readonly IMongoContext _mongoContext = mongoContext;
    
    public async Task<List<Employee>> GetAll(int skip = 0, int limit = 50)
    {
        return await _mongoContext.Employees.Find(u => true).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<List<Employee>> GetAll(Expression<Func<Employee, bool>> expression, int skip = 0, int limit = 50)
    {
        return await _mongoContext.Employees.Find(expression).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<Employee> Get(Expression<Func<Employee, bool>> expression)
    {
        return await _mongoContext.Employees.FindSync(expression).FirstOrDefaultAsync();
    }

    public async Task Save(Employee entity)
    {
        await _mongoContext.Employees.InsertOneAsync(entity);
    }

    public async Task Update(Employee entity)
    {
        await _mongoContext.Employees.ReplaceOneAsync(u => u.id.Equals(entity.id), entity);
    }

    public async Task Delete(ObjectId Id)
    {
        await _mongoContext.Employees.DeleteOneAsync(u => u.id.Equals(Id));
    }
}