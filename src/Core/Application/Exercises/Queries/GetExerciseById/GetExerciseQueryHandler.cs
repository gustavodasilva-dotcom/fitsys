using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Exercises.Queries.GetExerciseById;

internal sealed class GetExerciseQueryHandler(IExercisesRepository exercisesRepository)
    : IRequestHandler<GetExerciseById, Exercise>
{
    private readonly IExercisesRepository _exercisesRepository = exercisesRepository;

    public async Task<Exercise> Handle(GetExerciseById request, CancellationToken cancellationToken)
    {
        return await _exercisesRepository.Get(p => p.uid == request.UID) ??
            throw new ExerciseNotFoundException(request.UID.ToString());
    }
}
