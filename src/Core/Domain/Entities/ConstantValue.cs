using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class ConstantValue : Entity
{
    public ConstantValue(ObjectId id, Guid uid, int value, string description)
        : base(id, uid)
    {
        this.value = value;
        this.description = description;
    }

    private ConstantValue()
    {
    }

    public int value { get; private set; }
    public string description { get; private set; }
}