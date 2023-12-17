using Domain.Entities.Partials;
using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Exercise : Entity
{
    public Exercise(ObjectId id, Guid uid, string name, QuillEditor steps, List<ConstantValue> muscleGroups, List<ConstantValue> gymEquipments, string image)
        : base(id, uid)
    {
        this.name = name;
        this.image = image;
        this.steps = steps;
        this.muscleGroups = muscleGroups;
        this.gymEquipments = gymEquipments;
    }

    private Exercise()
    {
    }

    public string name { get; private set; }
    public string image { get; private set; }
    public QuillEditor steps { get; private set; }
    public List<ConstantValue> muscleGroups { get; private set; }
    public List<ConstantValue> gymEquipments { get; private set; }

    public void SetName(string name) => this.name = name;
    public void SetImage(string image) => this.image = image;
    public void SetSteps(QuillEditor steps) => this.steps = steps;
    public void SetMuscleGroup(ConstantValue muscleGroup) => this.muscleGroups.Add(muscleGroup);
    public void SetGymEquipment(ConstantValue gymEquipment) => this.gymEquipments.Add(gymEquipment);
}