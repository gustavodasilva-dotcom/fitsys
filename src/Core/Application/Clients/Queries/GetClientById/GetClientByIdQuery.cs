using Domain.Entities;
using MediatR;

namespace Application.Clients.Queries.GetClientById
{
    public sealed record GetClientByIdQuery(Guid UID) : IRequest<Client>;
}
