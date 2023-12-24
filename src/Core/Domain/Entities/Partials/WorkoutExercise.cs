namespace Domain.Entities.Partials;

public sealed class WorkoutExercise
{
    public WorkoutExercise(Guid uid, Guid uidExercise, int sets, int reps, Exercise exercise)
    {
        this.uid = uid;
        this.uidExercise = uidExercise;
        this.sets = sets;
        this.reps = reps;
        this.exercise = exercise;
    }

    private WorkoutExercise()
    {
    }

    public Guid uid { get; private set; }
    public Guid uidExercise { get; private set; }
    public int sets { get; private set; }
    public int reps { get; private set; }
    public Exercise exercise { get; private set; }

    public void SetUIDExercise(Guid uidExercise) => this.uidExercise = uidExercise;
    public void SetSets(int sets) => this.sets = sets;
    public void SetReps(int reps) => this.sets = reps;
    public void SetExercise(Exercise exercise) => this.exercise = exercise;
}
