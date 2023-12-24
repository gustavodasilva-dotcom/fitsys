using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Partials;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.UpdateClient;

internal sealed class UpdateClientCommandHandler(IClientsRepository clientsRepository,
                                                 IExercisesRepository exercisesRepository)
    : IRequestHandler<UpdateClientCommand, Client>
{
    private readonly IClientsRepository _clientsRepository = clientsRepository;
    private readonly IExercisesRepository _exercisesRepository = exercisesRepository;

    public async Task<Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientsRepository.Get(p => p.uid == request.UID) ??
            throw new ClientNotFoundException(request.UID.ToString());

        client.SetWeight(request.Weight);
        client.SetHeight(request.Height);

        client.person.SetName(request.Name);
        client.person.SetBirthday(request.Birthday);
        client.person.SetProfile(request.Profile);

        foreach (var workout in client.workouts.Where(w => !request.Workouts.Any(rw => rw.uid == w.uid)).ToList())
            client.workouts.Remove(workout);
        foreach (var workout in request.Workouts)
        {
            Workout _workout = client.workouts.FirstOrDefault(w => w.uid == workout.uid);

            if (_workout == null)
            {
                _workout = new Workout(
                    id: ObjectId.GenerateNewId(),
                    uid: Guid.NewGuid(),
                    number: workout.number,
                    name: workout.name,
                    exercises: workout.exercises.Select(ex => new WorkoutExercise(
                        uid: Guid.NewGuid(),
                        uidExercise: ex.uidExercise,
                        sets: ex.sets,
                        reps: ex.reps,
                        exercise: _exercisesRepository.Get(e => e.uid == ex.uidExercise).Result
                    )).ToList()
                );
            }
            else
            {
                _workout.SetNumber(workout.number);
                _workout.SetName(workout.name);

                foreach (var exercise in _workout.exercises.Where(w => !workout.exercises.Any(wk => wk.uid == w.uid)).ToList())
                    _workout.exercises.Remove(exercise);
                foreach (var exercise in workout.exercises)
                {
                    WorkoutExercise _exercise = _workout.exercises.FirstOrDefault(w => w.uid == exercise.uid);

                    if (_exercise == null)
                    {
                        _workout.SetExercise(new WorkoutExercise(
                            uid: Guid.NewGuid(),
                            uidExercise: exercise.uidExercise,
                            sets: exercise.sets,
                            reps: exercise.reps,
                            exercise: _exercisesRepository.Get(e => e.uid == exercise.uidExercise).Result
                        ));
                    }
                    else
                    {
                        _exercise.SetUIDExercise(exercise.uidExercise);
                        _exercise.SetSets(exercise.sets);
                        _exercise.SetReps(exercise.reps);
                        _exercise.SetExercise(await _exercisesRepository.Get(e => e.uid == exercise.uidExercise));
                    }
                }
            }
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        client.user.SetEmail(request.Email);
        client.user.SetPassword(request.Password);
        
        await _clientsRepository.Update(client);
        
        return client;
    }
}
