namespace Domain.Entities.Partials;

public sealed class User
{
    public User(string email, string password)
    {
        this.email = email.Trim();
        this.password = password.Trim();
    }

    private User()
    {
    }

    public string email { get; private set; }
    public string password { get; private set; }

    public void SetEmail(string email) => this.email = email;

    public void SetPassword(string password) => this.password = password;
}
