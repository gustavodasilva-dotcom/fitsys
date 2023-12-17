using MediatR;
using System.Security.Claims;

namespace Application.Accounts.Commands.ExecuteLogin;

public sealed record ExecuteLoginCommand(string Email, string Password) : IRequest<List<Claim>>;
