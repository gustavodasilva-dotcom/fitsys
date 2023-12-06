using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Person : Entity
{
    public Person(ObjectId id, Guid uid, User user, Client client, string? profile = null)
        : base(id, uid)
    {
        this.user = user;
        this.client = client;
        this.profile = profile;
    }

    public Person(ObjectId id, Guid uid, User user)
        : base(id, uid)
    {
        this.user = user;
    }

    private Person()
    {
    }

    public User user { get; private set; }

    public Client client { get; private set; }

    public string? profile { get; private set; }
}
