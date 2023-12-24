namespace Domain.Models;

public class ClientInputModel
{
    public decimal weight { get; set; }
    public decimal height { get; set; }
    public PersonInputModel person { get; set; }
    public UserInputModel user { get; set; }
    public List<WorkoutInputModel> workouts { get; set; }
}
