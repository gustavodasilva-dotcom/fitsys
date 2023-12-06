using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.CreateClient;

internal sealed class CreateClientCommandHandler(IPersonsRepository personsRepository) : IRequestHandler<CreateClientCommand, ObjectId>
{
    private readonly IPersonsRepository _personsRepository = personsRepository;

    public async Task<ObjectId> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Person person = await _personsRepository.Get(p => p.user != null && p.user.email.Equals(request.Email.Trim()));
        
        if (person == null)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            ObjectId id = ObjectId.GenerateNewId();
            Guid uid = Guid.NewGuid();

            User user = new(id, uid, request.Name, request.Email, passwordHash);
            Client client = new(id, uid, request.Weight, request.Height, request.Birthday);
            person = new Person(id, uid, user, client, request.Profile);

            await _personsRepository.Save(person);
        }
        
        return person.id;
    }
}
