using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Clients.Queries.GetClientById
{
    internal sealed class GetClientQueryHandler(IPersonsRepository personsRepository) : IRequestHandler<GetClientByIdQuery, Person>
    {
        private readonly IPersonsRepository _personsRepository = personsRepository;

        public async Task<Person> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _personsRepository.Get(p => p.client != null && p.id == request.Id) ??
                throw new ClientNotFoundException(request.Id.ToString());
            return client;
        }
    }
}
