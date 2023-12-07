namespace Web.Models.Entities;

public class ClientInputModel
{
    public decimal weight { get; set; }

    public decimal height { get; set; }

    public PersonInputModel person { get; set; }

    public UserInputModel user { get; set; }
}
