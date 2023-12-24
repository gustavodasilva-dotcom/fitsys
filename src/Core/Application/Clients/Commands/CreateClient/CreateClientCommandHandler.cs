using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using Domain.Enums;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.CreateClient;

internal sealed class CreateClientCommandHandler(IClientsRepository clientsRepository,
                                                 IConstantsRepository constantsRepository,
                                                 IExercisesRepository exercisesRepository)
    : IRequestHandler<CreateClientCommand, ObjectId>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;
    private readonly IConstantsRepository _constantsRepository = constantsRepository;
    private readonly IExercisesRepository _exercisesRepository = exercisesRepository;

    public async Task<ObjectId> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientsRepository.Get(p => p.user.email.Equals(request.Email.Trim()));

        if (client != null)
            throw new ClientConflictException(request.Email.Trim());

        Person person = new(
            name: request.Name,
            birthday: request.Birthday,
            profile: request.Profile);

        var roles = await _constantsRepository.Get(c => c.key == (int)ConstantsEnum.Roles);

        User user = new(
            email: request.Email,
            password: BCrypt.Net.BCrypt.HashPassword(request.Password),
            role: roles.values.FirstOrDefault(r => r.value == (int)RolesEnum.Client));

        List<Workout> workouts = request.Workouts.Select(wk => new Workout(
            id: ObjectId.GenerateNewId(),
            uid: Guid.NewGuid(),
            number: wk.number,
            name: wk.name,
            exercises: wk.exercises.Select(ex => new WorkoutExercise(
                uid: Guid.NewGuid(),
                uidExercise: ex.uidExercise,
                sets: ex.sets,
                reps: ex.reps,
                exercise: _exercisesRepository.Get(e => e.uid == ex.uidExercise).Result
            )).ToList()
        )).ToList();

        client = new Client(
            id: ObjectId.GenerateNewId(),
            uid: Guid.NewGuid(),
            weight: request.Weight,
            height: request.Height,
            person,
            user,
            workouts);

        await _clientsRepository.Save(client);

        return client.id;
    }
}
