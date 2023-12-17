using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum MuscleGroupsEnum
{
    [Display(Name = "Calves")]
    Calves = 0,

    [Display(Name = "Hamstrings")]
    Hamstrings = 1,

    [Display(Name = "Quadriceps")]
    Quadriceps = 2,

    [Display(Name = "Glutes")]
    Glutes = 3,

    [Display(Name = "Biceps")]
    Biceps = 4,

    [Display(Name = "Triceps")]
    Triceps = 5,

    [Display(Name = "Forearms")]
    Forearms = 6,

    [Display(Name = "Trapezius")]
    Trapezius = 7,

    [Display(Name = "Latissimus Dorsi")]
    LatissimusDorsi = 8
}
