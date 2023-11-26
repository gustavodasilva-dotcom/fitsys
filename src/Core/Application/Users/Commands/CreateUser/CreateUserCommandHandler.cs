using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, ObjectId>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task<ObjectId> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User(ObjectId.GenerateNewId(), request.Name, request.Email, passwordHash);
            _userRepository.Save(user);
            return Task.FromResult(user.Id);
        }
    }
}
