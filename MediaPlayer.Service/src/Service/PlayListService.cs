using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.src.Service
{
    public class PlayListService : IMediaPlayerMonitor
    {
        private List<INotify> _observers = new List<INotify>();
        private IPlaylistRepository _playListRepository;
        private User _user;

        public PlayListService(IPlaylistRepository playlistRepo, User user)
        {
            _playListRepository = playlistRepo;
            _user = user;
        }

        public PlayList? CreateNewPlaylist(PlayListCreateDTO playListCreate)
        {
            if (playListCreate.OwnerId != _user.Id)
            {
                Notify("Cannot create playlist. Wrong owner id");
                return null;
            }
            var playListFactory = new PlayListFactory();
            var newPlayList = playListFactory.Create(playListCreate);
            if (newPlayList is not null)
            {
                _playListRepository.AddPlaylist(newPlayList);
                _user.AddPlaylist(newPlayList);
                Notify($"New playlist created:{newPlayList}.");
            }
            else
            {
                Notify($"Failed to create new playlist.");
            }

            return newPlayList;
        }

        public bool AddPlaylistById(Guid id)
        {
            PlayList? playList = _playListRepository.GetPlayListById(id);

            if (playList is not null)
            {
                _user.AddPlaylist(playList);
                Notify($"Added playlist:{playList}");
                return true;
            }
            else
            {
                Notify("Failed to add playlist: playlist not found.");
                return false;
            }
        }

        public bool AddMediaToPlayList(Guid playListId, Media media)
        {
            var playList = GetUserPlayListById(playListId);
            if (playList is null)
            {
                Notify("Cannot add media to playlist: playlist not found");
                return false;
            }
            playList.AddToList(media);
            Notify($"Media:{media.Title} added to playlist:{playList}");
            return true;
        }

        public PlayList? GetUserPlayListById(Guid id)
        {
            return _user._playlists.FirstOrDefault(p => p.Id == id);
        }

        public HashSet<PlayList> GetUserPlayLists()
        {
            return _user._playlists;
        }

        public bool DeletePlaylistById(Guid id)
        {
            var playList = GetUserPlayListById(id);
            if (playList is null)
            {
                Notify($"The playlist you want to remove does not exists.");
                return false;
            }
            _user._playlists.Remove(playList);
            Notify($"Successfully removed playlist:{playList}");
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
