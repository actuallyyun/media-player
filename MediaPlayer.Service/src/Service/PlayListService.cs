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
            var userPlaylist = _user.GetPlaylist();
            if (userPlaylist.FirstOrDefault(p => p.Name == playListCreate.Name) is not null)
            {
                Notify(
                    "Cannot create playlist. Playlist with the same name already exists in your collection"
                );
            }

            var playListFactory = new PlayListFactory();
            var newPlayList = playListFactory.Create(playListCreate);
            if (newPlayList is not null)
            {
                _playListRepository.Add(newPlayList);
                _playListRepository.AddToUserPlaylist(_user, newPlayList);
                Notify($"New playlist created:{newPlayList}.");
            }
            else
            {
                Notify($"Failed to create new playlist.");
            }

            return newPlayList;
        }

        public bool AddPlaylistById(Guid id) // add an existing playlist to user playlist collection
        {
            PlayList? playlistFound = _playListRepository.GetPlayListById(id);
            if (playlistFound is null)
            {
                Notify("Failed to add playlist: playlist not found.");
                return false;
            }
            var userPlaylist = _user.GetPlaylist();
            if (userPlaylist.FirstOrDefault(p => p.Id == id) is not null)
            {
                Notify("Cannot add. Playlist already exsits in your collection.");
                return false;
            }
            _playListRepository.AddToUserPlaylist(_user, playlistFound);
            Notify($"Added playlist:{playlistFound}");
            return true;
        }

        public bool AddMediaToPlayList(Guid playListId, Media media)
        {
            var playList = _user.GetPlaylist().FirstOrDefault(p => p.Id == playListId);
            if (playList is null)
            {
                Notify("Cannot add media to playlist: playlist not found in your collection.");
                return false;
            }
            if (_playListRepository.GetMediaInPlaylistById(playListId, media.Id) is not null)
            {
                Notify($"Cannot add media to playlist:{media} is already in your list.");
                return false;
            }
            _playListRepository.AddMediaToPlaylist(playList, media);
            Notify($"Media:{media.Title} added to playlist:{playList}");
            return true;
        }

        public bool RemovePlaylistById(Guid id)
        {
            var playlistFound = _playListRepository.GetUserPlaylistById(_user.Id, id);
            if (playlistFound is null)
            {
                Notify("Cannot remove. Playlist is not in your collection.");
                return false;
            }
            _playListRepository.RemoveFromUserPlaylist(_user, playlistFound);
            Notify($"Remove successfully. {playlistFound} is removed from your collection.");
            return true;
        }

        public bool RemoveMediaFromList(Guid listId, Guid mediaId)
        {
            var playList = _user.GetPlaylist().FirstOrDefault(p => p.Id == listId);
            if (playList is null)
            {
                Notify("Cannot remove media from playlist: playlist not found in your collection.");
                return false;
            }
            try
            {
                var mediaFound = _playListRepository.GetMediaInPlaylistById(listId, mediaId);
                if (mediaFound is null)
                {
                    Notify($"Cannot remove media from playlist:media not found in your list.");
                    return false;
                }
                _playListRepository.RemoveMediaFromPlaylist(playList, mediaFound);
                Notify($"Media:{mediaFound.Title} removed from playlist:{playList}");
                return true;
            }
            catch (Exception e)
            {
                Notify(e.Message);
                return false;
            }
        }

        public bool DeletePlaylistById(Guid id)
        {
            var playList = _user.GetPlaylist().FirstOrDefault(p => p.Id == id);
            // user can only delete her own playlists
            if (playList is null)
            {
                Notify($"The playlist you want to remove does not exists.");
                return false;
            }
            _playListRepository.RemoveFromUserPlaylist(_user, playList);
            _playListRepository.Remove(playList); // remove from repo
            Notify($"Successfully removed playlist:{playList}");
            return true;
        }

        public bool UpdatePlaylist(Guid id, string name)
        {
            var playlistFound = _playListRepository.GetUserPlaylistById(_user.Id, id);
            if (playlistFound is null)
            {
                Notify("Cannot update playlist. Playlist not found");
                return false;
            }
            var existingPlaylist = _playListRepository.GetUserPlaylistByName(_user.Id, name);
            if (existingPlaylist is not null)
            {
                Notify(
                    "Cannot update playlist. A playlist with the same name already exits. Choose another name."
                );
                return false;
            }
            _playListRepository.Update(playlistFound,name);
            Notify($"Update successfully. New playlist name:{name}");
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
