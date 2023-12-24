namespace Domain.Models;

public sealed class WorkoutExerciseInputModel
{
    public Guid uid { get; set; }
    public Guid uidExercise { get; set; }
    public int sets { get; set; }
    public int reps { get; set; }
}
