using Domain.Entities.Partials;
using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities;

public sealed class PersonalTrainer : Entity
{
    public PersonalTrainer(ObjectId id, Guid uid, List<ConstantValue> shifts, Person person, User user)
        : base(id, uid)
    {
        this.shifts = shifts;
        this.person = person;
        this.user = user;
    }

    private PersonalTrainer()
    {
    }

    public List<ConstantValue> shifts { get; private set; }
    public Person person { get; private set; }
    public User user { get; private set; }

    public void SetShift(ConstantValue shift) => this.shifts.Add(shift);
}