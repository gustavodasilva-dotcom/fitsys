using MongoDB.Bson;

namespace Domain.Primitives;

public abstract class Entity
{
    protected Entity(ObjectId id) => this.id = id;

    protected Entity()
    {
    }

    public ObjectId id { get; protected set; }
}
