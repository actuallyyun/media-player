using MediaPlayer.Core.src.Abstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class UserNotification : INotify
    {
        // Notification for users.
        public void Update(string message)
        {
            Console.WriteLine($"User notification:{message}");
        }
    }
}