using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler(IPersonsRepository personsRepository) : IRequestHandler<CreateUserCommand, ObjectId>
    {
        private readonly IPersonsRepository _personsRepository = personsRepository;

        public Task<ObjectId> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var id = ObjectId.GenerateNewId();
            var person = new Person(id, new User(id, request.Name, request.Email, passwordHash));
            _personsRepository.Save(person);
            return Task.FromResult(person.Id);
        }
    }
}
