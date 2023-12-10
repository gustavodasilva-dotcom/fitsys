using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Constants.Queries.GetConstantByEnum;

internal sealed class GetConstantQueryHandler(IConstantsRepository constantsRepository) :
    IRequestHandler<GetConstantByEnumQuery, Constant>
{
    private readonly IConstantsRepository _constantsRepository = constantsRepository;

    public async Task<Constant> Handle(GetConstantByEnumQuery request, CancellationToken cancellationToken)
    {
        return await _constantsRepository.Get(c => c.key == (int)request.Constant);
    }
}
