namespace Web.Models.Entities;

public class EmployeeInputModel
{
    public List<Guid> shifts { get; set; }
    public PersonInputModel person { get; set; }
    public UserInputModel user { get; set; }
}