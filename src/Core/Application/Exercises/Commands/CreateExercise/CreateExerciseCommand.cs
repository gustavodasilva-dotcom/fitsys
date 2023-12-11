using Domain.Entities.Partials;
using MediatR;
using MongoDB.Bson;

namespace Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(string Name, QuillEditor Steps, List<Guid> MuscleGroups)
    : IRequest<ObjectId>;