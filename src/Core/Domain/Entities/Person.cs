using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Person : Entity
{
    public Person(ObjectId id, User user, Client client)
        : base(id)
    {
        this.user = user;
        this.client = client;
    }

    public Person(ObjectId id, User user)
        : base(id)
    {
        this.user = user;
    }

    private Person()
    {
    }

    public User user { get; private set; }

    public Client client { get; private set; }
}
