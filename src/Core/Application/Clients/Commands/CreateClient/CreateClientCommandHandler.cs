using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using Domain.Enums;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.CreateClient;

internal sealed class CreateClientCommandHandler(IClientsRepository clientsRepository,
                                                 IConstantsRepository constantsRepository)
    : IRequestHandler<CreateClientCommand, ObjectId>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;
    private readonly IConstantsRepository _constantsRepository = constantsRepository;

    public async Task<ObjectId> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientsRepository.Get(p => p.user.email.Equals(request.Email.Trim()));

        if (client != null)
            throw new ClientConflictException(request.Email.Trim());

        Person person = new(
            name: request.Name,
            birthday: request.Birthday,
            profile: request.Profile);

        var roles = await _constantsRepository.Get(c => c.key == (int)ConstantsEnum.Roles);

        User user = new(
            email: request.Email,
            password: BCrypt.Net.BCrypt.HashPassword(request.Password),
            role: roles.values.FirstOrDefault(r => r.value == (int)RolesEnum.Client));
        
        client = new Client(
            id: ObjectId.GenerateNewId(),
            uid: Guid.NewGuid(),
            weight: request.Weight,
            height: request.Height,
            person: person,
            user: user);

        await _clientsRepository.Save(client);

        return client.id;
    }
}
