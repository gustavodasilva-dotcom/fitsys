using Domain.Entities.Partials;
using MediatR;
using MongoDB.Bson;

namespace Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(string Name, string Image, QuillEditor Steps, List<Guid> MuscleGroups, List<Guid> GymEquipments)
    : IRequest<ObjectId>;