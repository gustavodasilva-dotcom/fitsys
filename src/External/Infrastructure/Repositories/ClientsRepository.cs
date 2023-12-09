using Domain.Abstractions;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public sealed class ClientsRepository(IMongoContext mongoContext) : IClientsRepository
{
    private readonly IMongoContext _mongoContext = mongoContext;

    public async Task<List<Client>> GetAll(int skip = 0, int limit = 50)
    {
        return await _mongoContext.Clients.Find(u => true).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<List<Client>> GetAll(Expression<Func<Client, bool>> expression, int skip = 0, int limit = 50)
    {
        return await _mongoContext.Clients.Find(expression).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<Client> Get(Expression<Func<Client, bool>> expression)
    {
        return await _mongoContext.Clients.FindSync(expression).FirstOrDefaultAsync();
    }

    public async Task Save(Client entity)
    {
        await _mongoContext.Clients.InsertOneAsync(entity);
    }

    public async Task Update(Client entity)
    {
        await _mongoContext.Clients.ReplaceOneAsync(u => u.id.Equals(entity.id), entity);
    }

    public async Task Delete(ObjectId Id)
    {
        await _mongoContext.Clients.DeleteOneAsync(u => u.id.Equals(Id));
    }
}
