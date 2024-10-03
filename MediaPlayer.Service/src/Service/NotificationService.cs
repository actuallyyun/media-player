
using MediaPlayer.Core.src.Abstraction;

namespace MediaPlayer.Service.src.Service
{
    public class NotificationService : IMediaPlayerMonitor
    {

        private List<INotify> _observers { get ;}
        public NotificationService(List<INotify> observers)
        {
            _observers=observers;
        }
        public void Attach(INotify observer)
        {
            _observers.Add(observer);
        }

        public void Detach(INotify observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
             foreach (var observer in _observers)
            {
                Console.WriteLine(observer.ToString());
                observer.Update(message);
            }
        }
    }
}