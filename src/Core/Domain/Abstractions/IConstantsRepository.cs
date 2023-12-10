using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IConstantsRepository
{
    Task<Constant> Get(Expression<Func<Constant, bool>> expression);
}