using Domain.Enums;

namespace Web.Models.Entities;

public class PersonalTrainerInputModel
{
    public List<ShiftsEnum> shifts { get; set; }
    public PersonInputModel person { get; set; }
    public UserInputModel user { get; set; }
}