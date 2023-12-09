using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.PersonalTrainers.Commands.UpdatePersonalTrainer;

internal sealed class UpdatePersonalTrainerCommandHandler(IPersonalTrainersRepository personalTrainersRepository)
    : IRequestHandler<UpdatePersonalTrainerCommand, PersonalTrainer>
{
    private readonly IPersonalTrainersRepository _personalTrainersRepository = personalTrainersRepository;

    public async Task<PersonalTrainer> Handle(UpdatePersonalTrainerCommand request, CancellationToken cancellationToken)
    {
        PersonalTrainer personal = await _personalTrainersRepository.Get(p => p.uid == request.UID) ??
            throw new PersonalTrainerNotFoundException(request.UID.ToString());

        personal.shifts.Clear();
        
        foreach (ShiftsEnum shift in request.Shifts)
            personal.SetShift(shift);

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
