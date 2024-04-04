namespace MediaPlayer.Core.src.Abstraction
{
    public interface IMediaPlayerMonitor
    {
        void Attach(INotify observer);
        void Detach(INotify observer);
        void Notify(string message);
    }
}
