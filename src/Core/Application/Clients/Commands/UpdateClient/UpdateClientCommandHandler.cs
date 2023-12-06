using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Clients.Commands.UpdateClient;

internal sealed class UpdateClientCommandHandler(IPersonsRepository personsRepository) : IRequestHandler<UpdateClientCommand, Person>
{
    private readonly IPersonsRepository _personsRepository = personsRepository;

    public async Task<Person> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var person = await _personsRepository.Get(p => p.client != null && p.uid == request.UID) ??
            throw new ClientNotFoundException(request.UID.ToString());

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new(person.id, request.UID, request.Name, request.Email, passwordHash);
        Client client = new(person.id, request.UID, request.Weight, request.Height, request.Birthday);
        person = new Person(person.id, request.UID, user, client, request.Profile);
        
        await _personsRepository.Update(person);
        
        return person;
    }
}
