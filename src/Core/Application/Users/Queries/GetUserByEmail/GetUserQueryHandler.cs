using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserByEmail
{
    internal sealed class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByEmail(request.Email) ?? throw new Exception("");
            return user;
        }
    }
}
