using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Service.src.Service
{
    public class PlayListControlService
    {
        private PlayList _playList;
        private User _user;

        public PlayListControlService(PlayList playList,User user){
            _playList=playList;
            _user=user;
        }

        public void PausePlaylist()
        {
            _playList.IsPlaying = false;
            _playList.IsPaused = true;
            Console.WriteLine($"Playlist{_playList} paused");
        }

        public void PlayPlaylist()
        {
            _playList.IsPlaying = true;
            _playList.IsPaused = false;
            Console.WriteLine($"Playlist{_playList} is playing");
        }

        public void StopPlaylist()
        {
            _playList.IsPlaying = false;
            _playList.IsPaused = false;
            Console.WriteLine($"Playlist{_playList} is stopped.");
        }

        public bool ChangeVolumn(Media media, int volumn)
        {// TODO add check if media is part of the playlist

            if (volumn > 0 && volumn <= 100)
            {
                media.SetVolumn(volumn);
                return true;
            }
            else
            {
                Console.WriteLine("Invalid volumn");
                return false;
            }
        }

        public bool ChangeSoundEffect(Media media, string soundAffect)
        {// TODO add check if media is part of the playlist
            if (media is Audio)
            {
                var audio = (Audio)media;
                audio.SetSoundAffect(soundAffect);
                Console.WriteLine($"Sound affect is set to {audio.SoundAffect}");
                return true;
            }
            else
            {
                Console.WriteLine($"Cannot set soundAffect on non audios.");
                return false;
            }
        }

        public bool ChangeBrightness(Media media, string brightness)
        {// TODO add check if media is part of the playlist
            if (media is Video)
            {
                var video = (Video)media;
                video.SetBrightness(brightness);
                Console.WriteLine($"Brightness is set to {video.Brightness}");
                return true;
            }
            else
            {
                Console.WriteLine($"Cannot set brightness on non videos.");

                return false;
            }
        }


    }
}