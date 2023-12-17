using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Queries.GetAllEmployees;

internal sealed class GetEmployeesQueryHandler(IEmployeesRepository employeesRepository)
    : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;

    public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _employeesRepository.GetAll();
    }
}
