using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Clients.Queries.GetClientById;

internal sealed class GetClientQueryHandler(IClientsRepository clientsRepository)
    : IRequestHandler<GetClientByIdQuery, Client>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;

    public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientsRepository.Get(p => p.uid == request.UID) ??
            throw new ClientNotFoundException(request.UID.ToString());

        return client;
    }
}
