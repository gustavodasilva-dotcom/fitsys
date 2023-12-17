using Application.Exercises.Commands.CreateExercise;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
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
        List<ConstantValue> gymEquipments = [];

        foreach (var muscleGroupId in request.MuscleGroups)
        {
            Constant constant = await _constantsRepository.Get(e => e.key == (int)ConstantsEnum.MuscleGroups);
            muscleGroups.Add(constant.values.FirstOrDefault(c => c.uid == muscleGroupId)!);
        }
        foreach (var equipmentId in request.GymEquipments)
        {
            Constant constant = await _constantsRepository.Get(e => e.key == (int)ConstantsEnum.GymEquipments);
            gymEquipments.Add(constant.values.FirstOrDefault(c => c.uid == equipmentId)!);
        }

        Exercise exercise = new(
            id: ObjectId.GenerateNewId(),
            uid: Guid.NewGuid(),
            name: request.Name,
            image: request.Image,
            steps: request.Steps,
            muscleGroups: muscleGroups,
            gymEquipments: gymEquipments);

        await _exercisesRepository.Save(exercise);

        return exercise.id;
    }
}
