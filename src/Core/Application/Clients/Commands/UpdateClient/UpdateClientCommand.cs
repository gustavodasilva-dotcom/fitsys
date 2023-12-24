using Domain.Entities;
using Domain.Models;
using MediatR;

namespace Application.Clients.Commands.UpdateClient;

public sealed record UpdateClientCommand(
    Guid UID,
    string Name,
    string Email,
    string Password,
    decimal Weight,
    decimal Height,
    DateTime Birthday,
    string? Profile,
    List<WorkoutInputModel> Workouts) : IRequest<Client>;