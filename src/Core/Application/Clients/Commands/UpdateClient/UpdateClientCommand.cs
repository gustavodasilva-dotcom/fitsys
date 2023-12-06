using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.UpdateClient;

public sealed record UpdateClientCommand(Guid UID, string Name, string Email, string Password, decimal Weight, decimal Height, DateTime Birthday, string? Profile = null)
    : IRequest<Person>;