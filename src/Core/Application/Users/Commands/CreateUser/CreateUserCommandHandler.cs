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

            ObjectId id = ObjectId.GenerateNewId();
            Guid uid = Guid.NewGuid();
            
            User user = new(id, uid, request.Name, request.Email, passwordHash);
            Person person = new(id, uid, user);

            _personsRepository.Save(person);
            
            return Task.FromResult(person.id);
        }
    }
}
