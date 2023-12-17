using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Commands.UpdateEmployee;

internal sealed class UpdateEmployeeCommandHandler(IEmployeesRepository employeesRepository,
                                                   IConstantsRepository constantsRepository)
    : IRequestHandler<UpdateEmployeeCommand, Employee>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;
    private readonly IConstantsRepository _constantsRepository = constantsRepository;

    public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeesRepository.Get(p => p.uid == request.UID) ??
            throw new EmployeeNotFoundException(request.UID.ToString());

        employee.shifts.Clear();
        
        List<ConstantValue> shifts = [];

        foreach (var shiftId in request.Shifts)
        {
            Constant constant = await _constantsRepository.Get(e => e.values.Any(e => e.uid == shiftId));
            employee.SetShift(constant.values.FirstOrDefault(c => c.uid == shiftId)!);
        }

        employee.person.SetName(request.Name);
        employee.person.SetBirthday(request.Birthday);
        employee.person.SetProfile(request.Profile);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        employee.user.SetEmail(request.Email);
        employee.user.SetPassword(request.Password);

        await _employeesRepository.Update(employee);

        return employee;
    }
}
