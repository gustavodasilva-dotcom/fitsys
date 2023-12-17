using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Queries.GetEmployeeById;

internal sealed class GetEmployeeQuery(IEmployeesRepository employeesRepository)
    : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;

    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _employeesRepository.Get(p => p.uid == request.UID) ??
            throw new EmployeeNotFoundException(request.UID.ToString());
    }
}
