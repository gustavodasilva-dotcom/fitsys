using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserByEmail
{
    internal sealed class GetUserQueryHandler(IPersonsRepository personsRepository) : IRequestHandler<GetUserByEmailQuery, Person>
    {
        private readonly IPersonsRepository _personsRepository = personsRepository;

        public Task<Person> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _personsRepository.Get(p => p.user != null && p.user.email.Trim().Equals(request.Email.Trim()))
                ?? throw new UserNotFoundException(request.Email);
            return user;
        }
    }
}
