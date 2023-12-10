using System.Linq.Expressions;
using Domain.Abstractions;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public sealed class ConstantsRepository(IMongoContext mongoContext) : IConstantsRepository
{
    private readonly IMongoContext _mongoContext = mongoContext;

    public async Task<Constant> Get(Expression<Func<Constant, bool>> expression)
    {
        return await _mongoContext.Constants.FindSync(expression).FirstOrDefaultAsync();
    }
}