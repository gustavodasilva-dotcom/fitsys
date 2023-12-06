namespace Web.Models.Entities;

public class PersonInputModel
{
    public UserInputModel user { get; set; }

    public ClientInputModel client { get; set; }

    public string? profile { get; set; }
}
