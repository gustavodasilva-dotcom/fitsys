using MongoDB.Bson;

namespace Domain.Primitives
{
    public abstract class Entity
    {
        protected Entity(ObjectId id) => Id = id;

        protected Entity()
        {
        }

        public ObjectId Id { get; protected set; }
    }
}
