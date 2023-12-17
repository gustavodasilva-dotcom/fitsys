using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using Domain.Security;
using MediatR;
using System.Security.Claims;

namespace Application.Accounts.Commands.ExecuteLogin;

public sealed class ExecuteLoginCommandHandler(IClientsRepository clientsRepository,
                                               IEmployeesRepository employeesRepository)
    : IRequestHandler<ExecuteLoginCommand, List<Claim>>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;

    public async Task<List<Claim>> Handle(ExecuteLoginCommand request, CancellationToken cancellationToken)
    {
        User? user = null;
        List<Claim> claims = [];

        Client client = await _clientsRepository.Get(c => c.user != null && c.user.email.Equals(request.Email.Trim().ToLower()));
        if (client != null)
        {
            claims.AddRange(new List<Claim>
            {
                new(ClaimTypes.Name, client.person.name),
                new(ClaimTypes.Role, client.user.role.description),
                new(CustomClaimTypes.Profile, client.person.profile ?? string.Empty)
            });
            user = client.user;
        }
        else
        {
            var employee = await _employeesRepository.Get(p => p.user != null && p.user.email.Equals(request.Email.Trim().ToLower()));

            if (employee != null)
            {
                claims.AddRange(new List<Claim>
                {
                    new(ClaimTypes.Name, employee.person.name),
                    new(ClaimTypes.Role, employee.user.role.description),
                    new(CustomClaimTypes.Profile, employee.person.profile ?? string.Empty)
                });
                user = employee.user;
            }
        }

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password.Trim(), user.password))
            throw new UserNotFoundException(request.Email);

        claims.AddRange(new List<Claim>()
        {
            new(ClaimTypes.Email, user.email)
        });

        return claims;
    }
}
