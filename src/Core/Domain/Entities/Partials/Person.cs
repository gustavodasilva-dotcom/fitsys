namespace Domain.Entities.Partials;

public sealed class Person
{
    public Person(string name, DateTime birthday, string? profile)
    {
        this.name = name;
        this.birthday = birthday;
        this.profile = profile;
    }

    private Person()
    {
    }

    public string name { get; private set; }
    public DateTime birthday { get; private set; }
    public string? profile { get; private set; }

    public void SetName(string name) => this.name = name;

    public void SetBirthday(DateTime birthday) => this.birthday = birthday;

    public void SetProfile(string? profile) => this.profile = profile;
}
