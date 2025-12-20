namespace app.Model;

class Manager(string username) : IUser
{
    public string Username { get; init; } = username;
    private readonly string _password = "admin"; // hardcoded for simplicity

    public bool Authorize(string password) => password == _password;
    
    public void ExecuteCommand(string command)
    {
        throw new NotImplementedException();
    }

    public void ShowCommands()
    {
        throw new NotImplementedException();
    }
}