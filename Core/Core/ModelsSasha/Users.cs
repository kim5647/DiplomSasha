namespace Core.Core.ModelsSasha;
public class Users
{
    public Users() { }
    public Users(int? userID, string login, string password)
    {
        UserID = userID;
        Login = login;
        Password = password;
    }
    public Users(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public int? UserID { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
