namespace app;

using app.Model;

class Program
{
    enum UserType
    {
        Passenger = 1,
        Manager
    }

    static IUser? GetUser(UserType type)
    {
        string username = Console.ReadLine() ?? "default_username";
        IUser user = type switch
        {
            UserType.Passenger => new Passenger(username),
            UserType.Manager => new Manager(username),
            _ => throw new ArgumentException("Invalid user type")
        };
        if (type == UserType.Manager)
        {
            Console.WriteLine("\ninsert password:");
            string password = Console.ReadLine() ?? "";
            if (!user.Authorize(password))
            {
                Console.WriteLine(@"wrong password for ""admin""");
                return null;
            }
        }
        return user;
    }

    static void Main(string[] args)
    {
        IUser? user = null;
        while (user is null)
        {
            Console.WriteLine("\nType 1 or 2 to select an user type:\n  1.passenger\n  2.manager:");
            var userType = Console.ReadLine() ?? "0";
            UserType userTypeCode = userType switch
            {
                "1" => UserType.Passenger,
                "2" => UserType.Manager,
                _ => 0
            };
            user = GetUser(userTypeCode);
        }
        

        while (true)
        {
            Console.WriteLine("insert commands for user, type help to list commands");
            string? command = Console.ReadLine() ?? "";
            if (command.Equals("help", StringComparison.CurrentCultureIgnoreCase))
                user.ShowCommands();
            else
                user.ExecuteCommand(command);
        }

    }
}