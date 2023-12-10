using Domain.Entities.Partials;
using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Constant : Entity
{
    public Constant(ObjectId id, Guid uid, int key, string name)
        : base(id, uid)
    {
        this.key = key;
        this.name = name;
        this.values = [];
    }

    private Constant()
    {
    }

    public int key { get; private set; }
    public string name { get; private set; }
    public List<ConstantValue> values { get; private set; }
}