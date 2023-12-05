using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class Client : Entity
{
    public Client(ObjectId id, decimal weight, decimal height, DateTime birthday)
        : base(id)
    {
        this.weight = weight;
        this.weight = height;
        this.birthday = birthday;
    }

    private Client()
    {
    }

    public decimal weight { get; private set; }

    public decimal height { get; private set; }

    public DateTime birthday { get; private set; }
}
