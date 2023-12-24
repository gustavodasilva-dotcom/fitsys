namespace Domain.Models;

public class ExerciseInputModel
{
    public string name { get; set; }
    public string image { get; set; }
    public QuillEditorInputModel steps { get; set; }
    public List<Guid> muscleGroups { get; set; }
    public List<Guid> gymEquipments { get; set; }
}
