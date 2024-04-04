using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Service.src.Service
{
    public class PlayListControlService : IMediaPlayerMonitor
    {
        private List<INotify> _observers = new List<INotify>();
        private PlayList _playList;
        private User _user;

        public PlayListControlService(PlayList playList, User user)
        {
            _playList = playList;
            _user = user;
            Notify($"A new playlist control service is created by user :{_user}");
        }

        public void PausePlaylist()
        {
            _playList.IsPlaying = false;
            _playList.IsPaused = true;
            Notify($"Playlist{_playList} paused");
        }

        public void PlayPlaylist()
        {
            _playList.IsPlaying = true;
            _playList.IsPaused = false;
            Notify($"Playlist{_playList} is playing");
        }

        public void StopPlaylist()
        {
            _playList.IsPlaying = false;
            _playList.IsPaused = false;
            Notify($"Playlist{_playList} is stopped.");
        }

        public bool ChangeVolumn(Media media, int volumn)
        { // TODO add check if media is part of the playlist
            if (volumn < 0 || volumn > 100)
            {
                Notify("Cannot set valoum: invalid volumn.");
                return false;
            }
            media.SetVolumn(volumn);
            Notify($"Volumn is set to:{volumn}.");
            return true;
        }

        public bool ChangeSoundEffect(Media media, string soundAffect)
        { // TODO add check if media is part of the playlist
            if (media is not Audio)
            {
                Notify($"Cannot set soundAffect on non audios.");
                return false;
            }

            var audio = (Audio)media;
            audio.SetSoundAffect(soundAffect);
            Notify($"Sound affect is set to {audio.SoundAffect}");
            return true;
        }

        public bool ChangeBrightness(Media media, string brightness)
        { // TODO add check if media is part of the playlist
            if (media is not Video)
            {
                Notify($"Cannot set brightness on non videos.");

                return false;
            }

            var video = (Video)media;
            video.SetBrightness(brightness);
            Notify($"Brightness is set to {video.Brightness}");
            return true;
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
                observer.Update(message);
            }
        }
    }
}
