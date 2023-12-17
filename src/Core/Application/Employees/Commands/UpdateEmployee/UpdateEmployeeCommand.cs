using Domain.Entities;
using MediatR;

namespace Application.Employees.Commands.UpdateEmployee;

public sealed record UpdateEmployeeCommand(Guid UID, List<Guid> Shifts, string Name, DateTime Birthday, string? Profile, string Email, string Password)
    : IRequest<Employee>;