using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class ConstantValue : Entity
{
    public ConstantValue(ObjectId id, Guid uid, string value)
        : base(id, uid)
    {
        this.value = value;
    }

    private ConstantValue()
    {
    }

    public string value { get; private set; }
}