using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.CreateClient;

internal sealed class CreateClientCommandHandler(IClientsRepository clientsRepository)
    : IRequestHandler<CreateClientCommand, ObjectId>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;

    public async Task<ObjectId> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientsRepository.Get(p => p.user.email.Equals(request.Email.Trim()));

        if (client != null)
            throw new ClientConflictException(request.Email.Trim());

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        ObjectId id = ObjectId.GenerateNewId();
        Guid uid = Guid.NewGuid();

        Person person = new(request.Name, request.Birthday, request.Profile);
        User user = new(request.Email, passwordHash);
        
        client = new Client(id, uid, request.Weight, request.Height, person, user);

        await _clientsRepository.Save(client);

        return client.id;
    }
}
