using Domain.Abstractions;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserRepository(IMongoContext mongoContext) : IUserRepository
    {
        private readonly IMongoContext _mongoContext = mongoContext;

        public async Task<IEnumerable<User>> GetAll(int skip = 0, int limit = 50)
        {
            return await _mongoContext.Users.Find(u => true).Skip(skip).Limit(limit).ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _mongoContext.Users.FindSync(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task Save(User entity)
        {
            await _mongoContext.Users.InsertOneAsync(entity);
        }

        public async Task Update(User entity)
        {
            await _mongoContext.Users.ReplaceOneAsync(u => u.Id.Equals(entity.Id), entity);
        }

        public async Task Delete(ObjectId Id)
        {
            await _mongoContext.Users.DeleteOneAsync(u => u.Id.Equals(Id));
        }
    }
}
