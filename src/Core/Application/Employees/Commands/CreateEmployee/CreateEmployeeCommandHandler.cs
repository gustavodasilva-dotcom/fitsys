using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using Domain.Enums;
using MediatR;
using MongoDB.Bson;

namespace Application.Employees.Commands.CreateEmployee;

internal sealed class CreateEmployeeCommandHandler(IEmployeesRepository employeesRepository,
                                                   IConstantsRepository constantsRepository) :
    IRequestHandler<CreateEmployeeCommand, ObjectId>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;
    private readonly IConstantsRepository _constantsRepository = constantsRepository;

    public async Task<ObjectId> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeesRepository.Get(p => p.user.email.Equals(request.Email.Trim()));

        if (employee != null)
            throw new EmployeeConflictException(request.Email.Trim());

        Person person = new(
            name: request.Name,
            birthday: request.Birthday,
            profile: request.Profile);

        var roles = await _constantsRepository.Get(c => c.key == (int)ConstantsEnum.Roles);

        User user = new(
            email: request.Email,
            password: BCrypt.Net.BCrypt.HashPassword(request.Password),
            role: roles.values.FirstOrDefault(r => r.value == (int)RolesEnum.PersonalTrainer));

        List<ConstantValue> shifts = [];

        foreach (var shiftId in request.Shifts)
        {
            Constant constant = await _constantsRepository.Get(e => e.values.Any(e => e.uid == shiftId));
            shifts.Add(constant.values.FirstOrDefault(c => c.uid == shiftId)!);
        }

        employee = new Employee(
            id: ObjectId.GenerateNewId(),
            uid: Guid.NewGuid(),
            shifts: shifts,
            person: person,
            user: user);

        await _employeesRepository.Save(employee);

        return employee.id;
    }
}
