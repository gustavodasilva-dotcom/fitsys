using MongoDB.Bson;

namespace Domain.Primitives;

public abstract class Entity
{
    protected Entity(ObjectId id, Guid uid) 
    {
        this.id = id;
        this.uid = uid;
    }

    protected Entity()
    {
    }

    public ObjectId id { get; protected set; }

    public Guid uid { get; protected set; }
}
