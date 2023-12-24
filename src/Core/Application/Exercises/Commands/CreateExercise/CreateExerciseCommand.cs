using Domain.Models;
using MediatR;
using MongoDB.Bson;

namespace Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(
    string Name,
    string Image,
    QuillEditorInputModel Steps,
    List<Guid> MuscleGroups,
    List<Guid> GymEquipments) : IRequest<ObjectId>;