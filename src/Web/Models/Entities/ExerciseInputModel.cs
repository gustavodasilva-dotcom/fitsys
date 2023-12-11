namespace Web.Models.Entities;

public class ExerciseInputModel
{
    public string name { get; set; }
    public QuillEditorInputModel steps { get; set; }
    public List<Guid> muscleGroups { get; set; }   
}