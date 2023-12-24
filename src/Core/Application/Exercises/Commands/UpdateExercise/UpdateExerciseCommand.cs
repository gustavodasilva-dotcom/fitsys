using Domain.Entities;
using Domain.Models;
using MediatR;

namespace Application.Exercises.Commands.UpdateExercise;

public sealed record UpdateExerciseCommand(
    Guid UID,
    string Name,
    string Image,
    QuillEditorInputModel Steps,
    List<Guid> MuscleGroups,
    List<Guid> GymEquipments) : IRequest<Exercise>;