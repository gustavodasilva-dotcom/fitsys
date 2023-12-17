using Domain.Entities;
using MediatR;

namespace Application.Employees.Queries.GetAllEmployees;

public sealed record GetAllEmployeesQuery(int Skip = 0, int Limit = 50) : IRequest<List<Employee>>;