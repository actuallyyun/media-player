namespace MediaPlayer.Core.Entity
{
    public class MediaPlayer
    {
        public string Name { get; set; }
        public int Volume { get; private set; }
        public bool IsPlaying { get; private set; }
        public PlayList Track;

        public MediaPlayer(PlayList track)// create a media player by providing a playlist
        {
            Name = track.Name;// User playlist name for media player
            Volume = 50; // Default volume
            IsPlaying = true; // Start playing once the player is created.
            Track=track;
        }

        public void Play()
        {
            Console.WriteLine($"{Name} is now playing.");
            IsPlaying = true;
        }

        public void Pause()
        {
            Console.WriteLine($"{Name} is now paused.");
            IsPlaying = false;
        }

        public void Stop()
        {
            Console.WriteLine($"{Name} is now stopped.");
            IsPlaying = false;
        }
    }
}
