namespace Domain.Entities.Partials;

public sealed class User
{
    public User(string email, string password, ConstantValue role)
    {
        this.email = email.Trim();
        this.password = password.Trim();
        this.role = role;
    }

    private User()
    {
    }

    public string email { get; private set; }
    public string password { get; private set; }
    public ConstantValue role { get; private set; }

    public void SetEmail(string email) => this.email = email;

    public void SetPassword(string password) => this.password = password;

    public void SetRoles(ConstantValue role) => this.role = role;
}
