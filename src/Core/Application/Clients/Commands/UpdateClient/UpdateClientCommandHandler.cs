using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Clients.Commands.UpdateClient;

internal sealed class UpdateClientCommandHandler(IClientsRepository clientsRepository)
    : IRequestHandler<UpdateClientCommand, Client>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;

    public async Task<Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientsRepository.Get(p => p.uid == request.UID) ??
            throw new ClientNotFoundException(request.UID.ToString());

        client.SetWeight(request.Weight);
        client.SetHeight(request.Height);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        client.person.SetName(request.Name);
        client.person.SetBirthday(request.Birthday);
        client.person.SetProfile(request.Profile);

        client.user.SetEmail(request.Email);
        client.user.SetPassword(request.Password);
        
        await _clientsRepository.Update(client);
        
        return client;
    }
}
