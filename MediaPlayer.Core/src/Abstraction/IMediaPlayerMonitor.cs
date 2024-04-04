namespace MediaPlayer.Core.src.Abstraction
{
    // interface for the observer
    public interface IMediaPlayerMonitor
    {
        void Attach(INotify observer);
        void Detach(INotify observer);
        void Notify(string message);
    }
}
