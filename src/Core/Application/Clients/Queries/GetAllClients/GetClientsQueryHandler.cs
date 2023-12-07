using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Clients.Queries.GetAllClients;

internal sealed class GetClientsQueryHandler(IClientsRepository clientsRepository)
    : IRequestHandler<GetAllClientsQuery, List<Client>>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;

    public async Task<List<Client>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        return await _clientsRepository.GetAll();
    }
}
