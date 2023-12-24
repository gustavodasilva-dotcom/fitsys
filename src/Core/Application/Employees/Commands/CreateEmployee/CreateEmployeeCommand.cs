using MediatR;
using MongoDB.Bson;

namespace Application.Employees.Commands.CreateEmployee;

public sealed record CreateEmployeeCommand(
    List<Guid> Shifts,
    string Name,
    DateTime Birthday,
    string? Profile,
    string Email,
    string Password) : IRequest<ObjectId>;