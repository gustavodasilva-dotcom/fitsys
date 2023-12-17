using Domain.Entities;
using MediatR;

namespace Application.Employees.Queries.GetEmployeeById;

public sealed record GetEmployeeByIdQuery(Guid UID) : IRequest<Employee>;