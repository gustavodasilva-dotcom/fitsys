using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.PersonalTrainers.Commands.UpdatePersonalTrainer;

public sealed record UpdatePersonalTrainerCommand(Guid UID, List<ShiftsEnum> Shifts, string Name, DateTime Birthday, string? Profile, string Email, string Password)
    : IRequest<PersonalTrainer>;