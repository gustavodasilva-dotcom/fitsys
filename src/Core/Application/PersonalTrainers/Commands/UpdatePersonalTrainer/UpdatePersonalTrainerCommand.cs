using Domain.Entities;
using MediatR;

namespace Application.PersonalTrainers.Commands.UpdatePersonalTrainer;

public sealed record UpdatePersonalTrainerCommand(Guid UID, List<Guid> Shifts, string Name, DateTime Birthday, string? Profile, string Email, string Password)
    : IRequest<PersonalTrainer>;