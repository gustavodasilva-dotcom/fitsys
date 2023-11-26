using Domain.Entities;
using MongoDB.Bson;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll(int skip = 0, int limit = 50);
        Task<User> GetByEmail(string email);
        Task Save(User entity);
        Task Update(User entity);
        Task Delete(ObjectId Id);
    }
}
