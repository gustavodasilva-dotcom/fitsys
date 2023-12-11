using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Exercises.Queries.GetAllExercises;

internal sealed class GetExercisesQueryHandler(IExercisesRepository exercisesRepository)
    : IRequestHandler<GetAllExercisesQuery, List<Exercise>>
{
    private readonly IExercisesRepository _exercisesRepository = exercisesRepository;

    public async Task<List<Exercise>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        return await _exercisesRepository.GetAll();
    }
}
