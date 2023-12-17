using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum GymEquipmentsEnum
{
    [Display(Name = "Chest Press Machine")]
    ChestPressMachine = 0,

    [Display(Name = "Seated Dip Machine")]
    SeatedDipMachine = 1,

    [Display(Name = "Chest Fly Machine")]
    ChestFlyMachine = 2,

    [Display(Name = "Bench Press")]
    BenchPress = 3,

    [Display(Name = "Incline Bench Press")]
    InclineBenchPress = 4,

    [Display(Name = "Decline Bench Press")]
    DeclineBenchPress = 5,

    [Display(Name = "Adjustable Bench")]
    AdjustableBench = 6,

    [Display(Name = "Olympic Weight Bench")]
    OlympicWeightBench = 7,

    [Display(Name = "Bicep Curl Bench")]
    BicepCurlBench = 8,

    [Display(Name = "Arm Curl Machine")]
    ArmCurlMachine = 9,

    [Display(Name = "Arm Extension Machine")]
    ArmExtensionMachine = 10,

    [Display(Name = "Triceps Press Machine")]
    TricepsPressMachine = 11,

    [Display(Name = "Tricep Extension Machine")]
    TricepExtensionMachine = 12,

    [Display(Name = "Shoulder Press Machine")]
    ShoulderPressMachine = 13,

    [Display(Name = "Overhead Press Machine")]
    OverheadPressMachine = 14,

    [Display(Name = "Lateral Raises Machine")]
    LateralRaisesMachine = 15,

    [Display(Name = "Back Extension Machine")]
    BackExtensionMachine = 16,

    [Display(Name = "Cable Row Machine")]
    CableRowMachine = 17,

    [Display(Name = "Lat Pull Down Machine")]
    LatPullDownMachine = 18,

    [Display(Name = "Glute Ham Developer")]
    GluteHamDeveloper = 17,

    [Display(Name = "Front Pull Down Machine")]
    FrontPullDownMachine = 18,

    [Display(Name = "Abdominal Bench")]
    AbdominalBench = 19,

    [Display(Name = "Ab Crunch Machine")]
    AbCrunchMachine = 20,

    [Display(Name = "Leg Raise Tower")]
    LegRaiseTower = 21,

    [Display(Name = "Ab Roller")]
    AbRoller = 22,

    [Display(Name = "Rotary Torso Machine")]
    RotaryTorsoMachine = 23,

    [Display(Name = "Leg Press Machine")]
    LegPressMachine = 24,

    [Display(Name = "Leg Extension Machine")]
    LegExtensionMachine = 25,

    [Display(Name = "Leg Curl Machine")]
    LegCurlMachine = 26,

    [Display(Name = "Leg Abduction Machine")]
    LegAbductionMachine = 27,

    [Display(Name = "Seated Calf Machine")]
    SeatedCalfMachine = 28,

    [Display(Name = "Standing Calf Machine")]
    StandingCalfMachine = 29,

    [Display(Name = "Calf Press Machine")]
    CalfPressMachine = 30,

    [Display(Name = "Butt Blaster Machine")]
    ButtBlasterMachine = 31,

    [Display(Name = "Hack Squat Machine")]
    HackSquatMachine = 32,

    [Display(Name = "Reverse Hyper Machine")]
    ReverseHyperMachine = 33,

    [Display(Name = "Kettlebells")]
    Kettlebells = 34,

    [Display(Name = "Dumbbells")]
    Dumbbells = 35,

    [Display(Name = "Barbells and Olympic Barbells")]
    BarbellsAndOlympicBarbells = 36,

    [Display(Name = "Medicine Ball")]
    MedicineBall = 37,

    [Display(Name = "Stability Ball")]
    StabilityBall = 38,

    [Display(Name = "Wallball")]
    Wallball = 39,

    [Display(Name = "Treadmill")]
    Treadmill = 40,

    [Display(Name = "Spin Bike")]
    SpinBike = 41,

    [Display(Name = "Air Bike")]
    AirBike = 42,

    [Display(Name = "Upright Exercise Bike")]
    UprightExerciseBike = 43,

    [Display(Name = "Recumbent Exercise Bike")]
    RecumbentExerciseBike = 44,

    [Display(Name = "Under Desk Bike")]
    UnderDeskBike = 45,

    [Display(Name = "Ellipticals")]
    Ellipticals = 46,

    [Display(Name = "Ski Erg")]
    SkiErg = 47,

    [Display(Name = "Vertical Climber")]
    VerticalClimber = 48,

    [Display(Name = "Stair Climber")]
    StairClimber = 49,

    [Display(Name = "Stepper")]
    Stepper = 50,

    [Display(Name = "Stepmill")]
    Stepmill = 51,

    [Display(Name = "Aerobic Steps")]
    AerobicSteps = 52,

    [Display(Name = "Smith Machine")]
    SmithMachine = 53,

    [Display(Name = "Rowing Machine")]
    RowingMachine = 54,

    [Display(Name = "Cable Crossover Machine")]
    CableCrossoverMachine = 55,

    [Display(Name = "Functional Trainer")]
    FunctionalTrainer = 56,

    [Display(Name = "Resistance Bands")]
    ResistanceBands = 57,

    [Display(Name = "Suspension Trainer")]
    SuspensionTrainer = 58,

    [Display(Name = "Punching Bag")]
    PunchingBag = 59,

    [Display(Name = "Climbing Rope")]
    ClimbingRope = 60,

    [Display(Name = "Battle Rope")]
    BattleRope = 61,

    [Display(Name = "Jump Rope / Skipping Rope")]
    JumpRopeSkippingRope = 62,

    [Display(Name = "Plyometric Box")]
    PlyometricBox = 63,

    [Display(Name = "Pull Up Bar")]
    PullUpBar = 64,

    [Display(Name = "Push-Up Bars")]
    PushUpBars = 65,

    [Display(Name = "Gymnastic Rings")]
    GymnasticRings = 66,

    [Display(Name = "Foam Roller")]
    FoamRoller = 68,

    [Display(Name = "Agility Ladder")]
    AgilityLadder = 69,

    [Display(Name = "Swiss Ball")]
    SwissBall = 70,

    [Display(Name = "Hand Grip Exerciser")]
    HandGripExerciser = 71
}
