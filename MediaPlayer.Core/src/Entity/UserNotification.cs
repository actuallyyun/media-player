using MediaPlayer.Core.src.Abstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class UserNotification : INotify
    {
        public void Update(string message)
        {
            Console.WriteLine($"User notification:{message}");
        }
    }
}