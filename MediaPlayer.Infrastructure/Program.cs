using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Infrastructure.src.Repository;
using MediaPlayer.Service.Service;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Out.WriteLine($"Welcome to media player.");
        Console.Out.WriteLine($"Initiating Database");
        var db = Database.GetDatabase();
        Console.Out.WriteLine($"Database initiated");
        Console.Out.WriteLine($"Initiating Notification Service");
        var notificationService = new NotificationService(new List<INotify>());
        Console.Out.WriteLine($"Notification Service initiated");
        Console.Out.WriteLine($"Creating an admin.");
        var admin = CreateAdmin();
        Console.Out.WriteLine($"Admin created.${admin}");
        Console.Out.WriteLine($"Initiating Authorization Service");
        var authorizationService = new AuthorizationService(admin);
        Console.Out.WriteLine($"Create an user repo");

        Console.Out.WriteLine($"Let's explore UserService.");
        Console.Out.WriteLine($"Create an user repo");

        var userRepo = new UserRepository(db);
        Console.Out.WriteLine($"Creating an UserService.");
        var adminUserService = new UserService(userRepo, notificationService, authorizationService);

        Console.Out.WriteLine($"Let's creating an user and add it to the database.");
        var userCreate = new UserCreateDto("testusr", "u@mail.com", "user test", UserType.User);
        Console.Out.WriteLine($"Creating new user...");
        var newUser = adminUserService.AddUser(userCreate);
        Console.Out.WriteLine($"User created: ${newUser}");
    }

    private static Admin CreateAdmin()
    {
        var admin = new Admin("admin", "admin@mail.com", "Admin");
        return admin;
    }
}
