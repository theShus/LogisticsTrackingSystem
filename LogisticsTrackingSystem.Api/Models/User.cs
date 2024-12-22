public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;  // In production, store hashed passwords
    public Role Role { get; set; }
}

public enum Role
{
	User,
	Admin
}