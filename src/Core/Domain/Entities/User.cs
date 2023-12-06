using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class User : Entity
{
    public User(ObjectId id, Guid uid, string name, string email, string password)
        : base(id, uid)
    {
        this.name = name.Trim();
        this.email = email.Trim();
        this.password = password.Trim();
    }

    private User()
    {
    }

    public string name { get; private set; }

    public string email { get; private set; }

    public string password { get; private set; }
}
