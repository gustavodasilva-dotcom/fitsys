using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using Domain.Enums;
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
        exercise.SetImage(request.Image);
        exercise.SetSteps(new QuillEditor()
        {
            ops = request.Steps.ops.Select(op => new Op()
            {
                insert = op.insert,
                attributes = op.attributes != null ? new Attributes()
                {
                    list = op.attributes.list
                } : null
            }).ToList()
        });

        exercise.muscleGroups.Clear();
        exercise.gymEquipments.Clear();

        foreach (var muscleGroupId in request.MuscleGroups)
        {
            Constant constant = await _constantsRepository.Get(e => e.key == (int)ConstantsEnum.MuscleGroups);
            exercise.SetMuscleGroup(constant.values.FirstOrDefault(c => c.uid == muscleGroupId)!);
        }
        foreach (var equipmentId in request.GymEquipments)
        {
            Constant constant = await _constantsRepository.Get(e => e.key == (int)ConstantsEnum.GymEquipments);
            exercise.SetGymEquipment(constant.values.FirstOrDefault(c => c.uid == equipmentId)!);
        }

        await _exercisesRepository.Update(exercise);

        return exercise;
    }
}
