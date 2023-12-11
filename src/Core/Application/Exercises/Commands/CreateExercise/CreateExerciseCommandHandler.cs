using Application.Exercises.Commands.CreateExercise;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;

internal sealed class CreateExerciseCommandHandler(IExercisesRepository exercisesRepository,
                                                   IConstantsRepository constantsRepository)
    : IRequestHandler<CreateExerciseCommand, ObjectId>
{
    private readonly IExercisesRepository _exercisesRepository = exercisesRepository;
    private readonly IConstantsRepository _constantsRepository = constantsRepository;

    public async Task<ObjectId> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        List<ConstantValue> muscleGroups = [];

        foreach (var muscleGroupId in request.MuscleGroups)
        {
            Constant constant = await _constantsRepository.Get(e => e.values.Any(e => e.uid == muscleGroupId));
            muscleGroups.Add(constant.values.FirstOrDefault(c => c.uid == muscleGroupId)!);
        }

        ObjectId id = ObjectId.GenerateNewId();
        Guid uid = Guid.NewGuid();

        Exercise exercise = new(id, uid, request.Name, request.Steps, muscleGroups);

        await _exercisesRepository.Save(exercise);

        return exercise.id;
    }
}
