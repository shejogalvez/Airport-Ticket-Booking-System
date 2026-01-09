namespace app.Model;

public interface IUser
{
    public string Username { get; init; }

    public bool Authorize(string password);

    public void ExecuteCommand(string command);

    public void ShowCommands();

}