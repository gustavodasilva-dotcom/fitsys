using MediatR;
using MongoDB.Bson;

namespace Application.PersonalTrainers.Commands.CreatePersonalTrainer;

public sealed record CreatePersonalTrainerCommand(List<Guid> Shifts, string Name, DateTime Birthday, string? Profile, string Email, string Password)
    : IRequest<ObjectId>;