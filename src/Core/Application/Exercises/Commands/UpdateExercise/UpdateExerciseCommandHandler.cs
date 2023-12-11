using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Exercises.Commands.UpdateExercise;

internal sealed class UpdateExerciseCommandHandler(IExercisesRepository exercisesRepository,
                                                   IConstantsRepository constantsRepository)
    : IRequestHandler<UpdateExerciseCommand, Exercise>
{
    private readonly IExercisesRepository _exercisesRepository = exercisesRepository;
    private readonly IConstantsRepository _constantsRepository = constantsRepository;

    public async Task<Exercise> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        Exercise exercise = await _exercisesRepository.Get(p => p.uid == request.UID) ??
            throw new ExerciseNotFoundException(request.UID.ToString());

        exercise.SetName(request.Name);
        exercise.SetSteps(request.Steps);

        exercise.muscleGroups.Clear();

        List<ConstantValue> muscleGroups = [];

        foreach (var muscleGroupId in request.MuscleGroups)
        {
            Constant constant = await _constantsRepository.Get(e => e.values.Any(e => e.uid == muscleGroupId));
            exercise.SetMuscleGroup(constant.values.FirstOrDefault(c => c.uid == muscleGroupId)!);
        }

        await _exercisesRepository.Update(exercise);

        return exercise;
    }
}
