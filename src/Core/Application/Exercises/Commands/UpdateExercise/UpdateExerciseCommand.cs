using Domain.Entities;
using Domain.Entities.Partials;
using MediatR;

namespace Application.Exercises.Commands.UpdateExercise;

public sealed record UpdateExerciseCommand(Guid UID, string Name, string Image, QuillEditor Steps, List<Guid> MuscleGroups, List<Guid> GymEquipments)
    : IRequest<Exercise>;