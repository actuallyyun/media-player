using MediaPlayer.Core.src.Abstraction;

namespace MediaPlayer.Service.src.Service
{
    public class Notifier : INotify
    {
        public void Update(string message)
        {
            Console.Out.WriteLine(message);
        }
    }
}