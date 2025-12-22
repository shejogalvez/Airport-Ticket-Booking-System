namespace app;

using app.Model;
using app.Utils;

class Program
{
    enum UserType
    {
        Passenger = 1,
        Manager
    }

    static IUser? GetUser(UserType type)
    {
        Console.WriteLine("\ninsert username:");
        string username = IOUtils.ReadInput() ?? "default_username";
        IUser user = type switch
        {
            UserType.Passenger => new Passenger(username),
            UserType.Manager => new Manager(username),
            _ => throw new ArgumentException("Invalid user type")
        };
        if (type == UserType.Manager)
        {
            Console.WriteLine("\ninsert password:");
            string password = IOUtils.ReadInput() ?? "";
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
        while (true){
            IUser? user = null;
            while (user is null)
            {
                Console.WriteLine("\nType 1 or 2 to select an user type:\n  1.passenger\n  2.manager");
                var userType = IOUtils.ReadInput() ?? "0";
                UserType userTypeCode = userType switch
                {
                    "1" => UserType.Passenger,
                    "2" => UserType.Manager,
                    _ => 0
                };
                user = GetUser(userTypeCode);
            }
            
            string? command = "something";
            while (command != "logout")
            {
                Console.WriteLine("insert commands for user, type help to list commands");
                command = IOUtils.ReadInput() ?? "";
                if (command == "logout") continue;
                if (command.Equals("help", StringComparison.CurrentCultureIgnoreCase))
                    user.ShowCommands();
                else
                    user.ExecuteCommand(command);
            }
        }
    }
}