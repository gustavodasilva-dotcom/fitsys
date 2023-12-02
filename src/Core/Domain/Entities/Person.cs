using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities
{
    public sealed class Person : Entity
    {
        public Person(ObjectId id, User user, Client client)
            : base(id)
        {
            User = user;
            Client = client;
        }

        public Person(ObjectId id, User user)
            : base(id)
        {
            User = user;
        }

        private Person()
        {
        }

        public User User { get; set; }

        public Client Client { get; set; }
    }
}
