using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.PersonalTrainers.Queries.GetAllPersonalTrainers;

internal sealed class GetPersonalTrainersQueryHandler(IPersonalTrainersRepository personalTrainersRepository)
    : IRequestHandler<GetAllPersonalTrainersQuery, List<PersonalTrainer>>
{
    private readonly IPersonalTrainersRepository _personalTrainersRepository = personalTrainersRepository;

    public async Task<List<PersonalTrainer>> Handle(GetAllPersonalTrainersQuery request, CancellationToken cancellationToken)
    {
        return await _personalTrainersRepository.GetAll();
    }
}
