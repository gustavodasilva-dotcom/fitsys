using Domain.Entities.Partials;
using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Workout : Entity
{
    public Workout(ObjectId id, Guid uid, int number, string name, List<WorkoutExercise> exercises)
        : base(id, uid)
    {
        this.number = number;
        this.name = name;
        this.exercises = exercises;
    }

    private Workout()
    {
    }

    public int number { get; private set; }
    public string name { get; private set; }
    public List<WorkoutExercise> exercises { get; private set; }

    public void SetNumber(int number) => this.number = number;
    public void SetName(string name) => this.name = name;
    public void SetExercise(WorkoutExercise exercise) => this.exercises.Add(exercise);
}
