using Domain.Entities.Partials;
using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Client : Entity
{
    public Client(ObjectId id, Guid uid, decimal weight, decimal height, Person person, User user)
        : base(id, uid)
    {
        this.weight = weight;
        this.height = height;
        this.person = person;
        this.user = user;
    }

    private Client()
    {
    }

    public decimal weight { get; private set; }

    public decimal height { get; private set; }

    public Person person { get; private set; }

    public User user { get; private set; }

    public void SetWeight(decimal weight) => this.weight = weight;

    public void SetHeight(decimal height) => this.height = height;
}
