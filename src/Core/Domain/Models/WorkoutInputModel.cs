namespace Domain.Models;

public sealed class WorkoutInputModel
{
    public Guid uid { get; set; }
    public int number { get; set; }
    public string name { get; set; }
    public List<WorkoutExerciseInputModel> exercises { get; set; }
}
