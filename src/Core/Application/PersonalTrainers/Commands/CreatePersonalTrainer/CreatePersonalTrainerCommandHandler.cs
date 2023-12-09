using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using MediatR;
using MongoDB.Bson;

namespace Application.PersonalTrainers.Commands.CreatePersonalTrainer;

internal sealed class CreateClientCommandHandler(IPersonalTrainersRepository personalTrainersRepository) :
    IRequestHandler<CreatePersonalTrainerCommand, ObjectId>
{
    private readonly IPersonalTrainersRepository _personalTrainersRepository = personalTrainersRepository;

    public async Task<ObjectId> Handle(CreatePersonalTrainerCommand request, CancellationToken cancellationToken)
    {
        PersonalTrainer personal = await _personalTrainersRepository.Get(p => p.user.email.Equals(request.Email.Trim()));

        if (personal != null)
            throw new PersonalTrainerConflictException(request.Email.Trim());

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        ObjectId id = ObjectId.GenerateNewId();
        Guid uid = Guid.NewGuid();

        Person person = new(request.Name, request.Birthday, request.Profile);
        User user = new(request.Email, passwordHash);

        personal = new PersonalTrainer(id, uid, request.Shifts, person, user);

        await _personalTrainersRepository.Save(personal);

        return personal.id;
    }
}
