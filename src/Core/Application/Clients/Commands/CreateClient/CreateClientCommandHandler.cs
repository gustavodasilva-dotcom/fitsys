using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.CreateClient
{
    internal sealed class CreateClientCommandHandler(IPersonsRepository personsRepository) : IRequestHandler<CreateClientCommand, ObjectId>
    {
        private readonly IPersonsRepository _personsRepository = personsRepository;

        public async Task<ObjectId> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var person = await _personsRepository.Get(p => p.user != null && p.user.email.Equals(request.Email.Trim()));
            if (person == null)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                var id = ObjectId.GenerateNewId();
                var user = new User(id, request.Name, request.Email, passwordHash);
                var client = new Client(id, request.Weight, request.Height, request.Birthday);
                person = new Person(id, user, client);
                await _personsRepository.Save(person);
            }
            return person.id;
        }
    }
}
