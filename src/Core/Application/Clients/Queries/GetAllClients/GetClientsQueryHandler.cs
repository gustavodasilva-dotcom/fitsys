using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Clients.Queries.GetAllClients
{
    internal sealed class GetClientsQueryHandler(IPersonsRepository personsRepository) : IRequestHandler<GetAllClientsQuery, List<Person>>
    {
        private readonly IPersonsRepository _personsRepository = personsRepository;

        public async Task<List<Person>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            return await _personsRepository.GetAll(p => p.client != null);
        }
    }
}
