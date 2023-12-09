using Domain.Enums;
using MediatR;
using MongoDB.Bson;

namespace Application.PersonalTrainers.Commands.CreatePersonalTrainer;

public sealed record CreatePersonalTrainerCommand(List<ShiftsEnum> Shifts, string Name, DateTime Birthday, string? Profile, string Email, string Password)
    : IRequest<ObjectId>;