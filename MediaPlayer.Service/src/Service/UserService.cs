using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.EntityAbstraction;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.Service
{
    public class UserService : IUserAction
    {
        // TODO not sure if it is secure to let userService access all playlists.
        private IPlaylistRepository _playListRepository;
        private User _user;

        public UserService(IPlaylistRepository playlistRepo, User user)
        {
            _playListRepository = playlistRepo;
            _user = user;
        }

        public PlayList CreateNewPlaylist(PlayListCreateDTO playListCreate)
        {
            var playListFactory = new PlayListFactory();
            var newPlayList = playListFactory.Create(playListCreate);
            if (newPlayList is not null)
            {
                _playListRepository.AddPlaylist(newPlayList);
                _user.AddPlaylist(newPlayList);
                Console.WriteLine($"New playlist created:{newPlayList}");
            }
            else
            {
                Console.WriteLine($"Failed to create new playlist.");
            }

            return newPlayList;
        }

        public bool AddPlaylistById(Guid id)
        {
            PlayList? playList = _playListRepository.GetPlayListById(id);

            if (playList is not null)
            {
                _user.AddPlaylist(playList);
                return true;
            }
            else
            {
                Console.WriteLine("Failed to add playlist.");
                return false;
            }
        }

        public bool AddMediaToPlayList(Guid playListId, Media media)
        {
            var playList = GetUserPlayListById(playListId);
            if (playList is not null)
            {
                playList.AddToList(media);
                Console.WriteLine($"Media:{media.Title} added to playlist:{playList}");
                return true;
            }
            else
            {
                return false;
            }
        }

        public PlayList? GetUserPlayListById(Guid id)
        {
            return _user._playlists.First(p => p.Id == id);
        }

        public HashSet<PlayList> GetUserPlayLists()
        {
            return _user._playlists;
        }

        public bool DeletePlaylistById(Guid id)
        {
            var playList = _user._playlists.FirstOrDefault(p => p.Id == id);
            if (playList is not null)
            {
                _user._playlists.Remove(playList);
                Console.WriteLine($"Successfully removed playlist:{playList}");
                return true;
            }
            else
            {
                Console.WriteLine($"The playlist you want to remove does not exists.");
                return false;
            }
        }

        public void PausePlaylist(PlayList playlist)
        {
            playlist.IsPlaying = false;
            playlist.IsPaused = true;
            Console.WriteLine($"Playlist{playlist} paused");
        }

        public void PlayPlaylist(PlayList playlist)
        {
            playlist.IsPlaying = true;
            playlist.IsPaused = false;
            Console.WriteLine($"Playlist{playlist} is playing");
        }

        public void StopPlaylist(PlayList playlist)
        {
            playlist.IsPlaying = false;
            playlist.IsPaused = false;
            Console.WriteLine($"Playlist{playlist} is stopped.");
        }

        public bool ChangeVolumn(Media media, int volumn)
        {
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
        {
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
        {
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
