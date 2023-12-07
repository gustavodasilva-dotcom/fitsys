using Domain.Entities;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Domain.Abstractions
{
    public interface IClientsRepository
    {
        Task<List<Client>> GetAll(int skip = 0, int limit = 50);
        Task<List<Client>> GetAll(Expression<Func<Client, bool>> expression, int skip = 0, int limit = 50);
        Task<Client> Get(Expression<Func<Client, bool>> expression);
        Task Save(Client entity);
        Task Update(Client entity);
        Task Delete(ObjectId Id);
    }
}
