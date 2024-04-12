using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;
using MediaPlayer.Service.src.ServiceAbstraction;
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Service.src.Service
{
    public class PlayListService 
    {
        private IPlaylistRepository _playListRepository;
        
        private readonly IMediaPlayerMonitor _notificationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly User _user;
        public PlayListService(IPlaylistRepository playlistRepo, IMediaPlayerMonitor notificationService, IAuthorizationService authorizationService)
        { 
            _playListRepository=playlistRepo;
            _notificationService=notificationService;
            if(!authorizationService.IsAuthenticated ||!authorizationService.HasPermission(UserType.User)){
                throw new UnauthorizedAccessException("Unauthorized action.");
            }
            _authorizationService = authorizationService;
            _user=_authorizationService.GetUser();
        }

        public PlayList? CreateNewPlaylist(PlayListCreateDTO playListCreate)
        {

            if (!playListCreate.OwnerId.Equals(_user.Id) )
            {
                _notificationService.Notify("Cannot create playlist. Wrong owner id");
                return null;
            }
            var userPlaylist = _playListRepository.GetPlaylistsByOwnerId(_user.Id);
            if (userPlaylist.FirstOrDefault(p => p.Name == playListCreate.Name) is not null)
            {
                Console.WriteLine("2");
                _notificationService.Notify(
                    "Cannot create playlist. Playlist with the same name already exists in your collection"
                );
            }

            var playListFactory = new PlayListFactory();
            var newPlayList = playListFactory.Create(playListCreate);
            Console.WriteLine($"new playlist:{newPlayList}");
            if (newPlayList is not null)
            {
                _playListRepository.Add(newPlayList);
                _playListRepository.AddToUserPlaylist(_user, newPlayList);
                _notificationService.Notify($"New playlist created:{newPlayList}.");
            }
            else
            {
                _notificationService.Notify($"Failed to create new playlist.");
            }

            return newPlayList;
        }

        public bool AddPlaylistById(Guid id) // add an existing playlist to user playlist collection
        {
            PlayList? playlistFound = _playListRepository.GetPlayListById(id);
            if (playlistFound is null)
            {
                _notificationService.Notify("Failed to add playlist: playlist not found.");
                return false;
            }
            var userPlaylist = _user.GetPlaylist();
            if (userPlaylist.FirstOrDefault(p => p.Id == id) is not null)
            {
                _notificationService.Notify("Cannot add. Playlist already exsits in your collection.");
                return false;
            }
            _playListRepository.AddToUserPlaylist(_user, playlistFound);
            _notificationService.Notify($"Added playlist:{playlistFound}");
            return true;
        }

        public bool AddMediaToPlayList(Guid playListId, Media media) // can only add to playlists she created
        {
            var playList = _playListRepository.GetPlaylistsByOwnerId(_user.Id).FirstOrDefault(p => p.Id == playListId);
            if (playList is null)
            {
                _notificationService.Notify("Cannot add media to playlist: playlist not found in your collection.");
                return false;
            }
            if (_playListRepository.GetMediaInPlaylistById(playListId, media.Id) is not null)
            {
                _notificationService.Notify($"Cannot add media to playlist:{media} is already in your list.");
                return false;
            }
            _playListRepository.AddMediaToPlaylist(playList, media);
            _notificationService.Notify($"Media:{media.Title} added to playlist:{playList}");
            return true;
        }

        public bool RemovePlaylistById(Guid id)
        {
            var playlistFound = _playListRepository.GetUserPlaylistById(_user.Id, id);
            if (playlistFound is null)
            {
                _notificationService.Notify("Cannot remove. Playlist is not in your collection.");
                return false;
            }
            _playListRepository.RemoveFromUserPlaylist(_user, playlistFound);
            _notificationService.Notify($"Remove successfully. {playlistFound} is removed from your collection.");
            return true;
        }

        public bool RemoveMediaFromList(Guid listId, Guid mediaId) // can only remove from playlists she created
        {
            var playList = _playListRepository.GetPlaylistsByOwnerId(_user.Id).FirstOrDefault(p => p.Id == listId);
            if (playList is null)
            {
                _notificationService.Notify("Cannot remove media from playlist: playlist not found in your collection.");
                return false;
            }
            try
            {
                var mediaFound = _playListRepository.GetMediaInPlaylistById(listId, mediaId);
                if (mediaFound is null)
                {
                    _notificationService.Notify($"Cannot remove media from playlist:media not found in your list.");
                    return false;
                }
                _playListRepository.RemoveMediaFromPlaylist(playList, mediaFound);
                _notificationService.Notify($"Media:{mediaFound.Title} removed from playlist:{playList}");
                return true;
            }
            catch (Exception e)
            {
                _notificationService.Notify(e.Message);
                return false;
            }
        }

        public bool DeletePlaylistById(Guid id) // can only delete playlists she created
        {
            var playList = _playListRepository.GetPlaylistsByOwnerId(_user.Id).FirstOrDefault(p => p.Id == id);
            // user can only delete her own playlists
            if (playList is null)
            {
                _notificationService.Notify($"The playlist you want to remove does not exists.");
                return false;
            }
            _playListRepository.RemoveFromUserPlaylist(_user, playList);
            _playListRepository.Remove(playList); // remove from repo
            _notificationService.Notify($"Successfully removed playlist:{playList}");
            return true;
        }

        public bool UpdatePlaylist(Guid id, string name)
        {
            var playlistFound = _playListRepository.GetUserPlaylistById(_user.Id, id);
            if (playlistFound is null)
            {
                _notificationService.Notify("Cannot update playlist. Playlist not found");
                return false;
            }
            var existingPlaylist = _playListRepository.GetUserPlaylistByName(_user.Id, name);
            if (existingPlaylist is not null)
            {
                _notificationService.Notify(
                    "Cannot update playlist. A playlist with the same name already exits. Choose another name."
                );
                return false;
            }
            _playListRepository.Update(playlistFound,name);
            _notificationService.Notify($"Update successfully. New playlist name:{name}");
            return true;
        }

    }
}
