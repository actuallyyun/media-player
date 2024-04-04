using MediaPlayer.Core.src.Abstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class AdminNotification : INotify
    {
        // Notification for admins
        public void Update(string message)
        {
            Console.WriteLine($"Admin notification:{message}");
        }
    }
}