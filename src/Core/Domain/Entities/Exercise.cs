using Domain.Entities.Partials;
using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Exercise : Entity
{
    public Exercise(ObjectId id, Guid uid, string name, QuillEditor steps, List<ConstantValue> muscleGroups)
        : base(id, uid)
    {
        this.name = name;
        this.steps = steps;
        this.muscleGroups = muscleGroups;
    }

    private Exercise()
    {
    }

    public string name { get; private set; }
    public QuillEditor steps { get; private set; }
    public List<ConstantValue> muscleGroups { get; set; }

    public void SetName(string name) => this.name = name;
    public void SetSteps(QuillEditor steps) => this.steps = steps;
    public void SetMuscleGroup(ConstantValue muscleGroup) => this.muscleGroups.Add(muscleGroup);
}