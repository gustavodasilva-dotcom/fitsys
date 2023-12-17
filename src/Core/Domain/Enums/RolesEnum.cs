using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum RolesEnum
{
    [Display(Name = "Admin")]
    Admin = 0,

    [Display(Name = "Personal Trainer")]
    PersonalTrainer = 1,

    [Display(Name = "Client")]
    Client = 2
}
