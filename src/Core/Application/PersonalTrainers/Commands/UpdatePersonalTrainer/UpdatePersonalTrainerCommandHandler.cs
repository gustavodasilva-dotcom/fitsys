using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.PersonalTrainers.Commands.UpdatePersonalTrainer;

internal sealed class UpdatePersonalTrainerCommandHandler(IPersonalTrainersRepository personalTrainersRepository,
                                                          IConstantsRepository constantsRepository)
    : IRequestHandler<UpdatePersonalTrainerCommand, PersonalTrainer>
{
    private readonly IPersonalTrainersRepository _personalTrainersRepository = personalTrainersRepository;
    private readonly IConstantsRepository _constantsRepository = constantsRepository;

    public async Task<PersonalTrainer> Handle(UpdatePersonalTrainerCommand request, CancellationToken cancellationToken)
    {
        PersonalTrainer personal = await _personalTrainersRepository.Get(p => p.uid == request.UID) ??
            throw new PersonalTrainerNotFoundException(request.UID.ToString());

        personal.shifts.Clear();
        
        List<ConstantValue> shifts = [];

        foreach (var shiftId in request.Shifts)
        {
            Constant constant = await _constantsRepository.Get(e => e.values.Any(e => e.uid == shiftId));
            personal.SetShift(constant.values.FirstOrDefault(c => c.uid == shiftId)!);
        }

        personal.person.SetName(request.Name);
        personal.person.SetBirthday(request.Birthday);
        personal.person.SetProfile(request.Profile);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        personal.user.SetEmail(request.Email);
        personal.user.SetPassword(request.Password);

        await _personalTrainersRepository.Update(personal);

        return personal;
    }
}
