using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.PersonalTrainers.Queries.GetPersonalTrainerById;

internal sealed class GetPersonalTrainerQuery(IPersonalTrainersRepository personalTrainersRepository)
    : IRequestHandler<GetPersonalTrainerByIdQuery, PersonalTrainer>
{
    private readonly IPersonalTrainersRepository _personalTrainersRepository = personalTrainersRepository;

    public async Task<PersonalTrainer> Handle(GetPersonalTrainerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _personalTrainersRepository.Get(p => p.uid == request.UID) ??
            throw new PersonalTrainerNotFoundException(request.UID.ToString());
    }
}
