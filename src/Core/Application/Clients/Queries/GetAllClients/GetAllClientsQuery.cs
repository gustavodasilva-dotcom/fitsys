using Domain.Entities;
using MediatR;

namespace Application.Clients.Queries.GetAllClients;

public sealed record GetAllClientsQuery(int Skip = 0, int Limit = 50) : IRequest<List<Client>>;
