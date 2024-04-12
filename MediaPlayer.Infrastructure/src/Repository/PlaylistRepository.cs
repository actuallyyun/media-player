using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Infrastructure.src.Data;

namespace MediaPlayer.Infrastructure.src.Repository
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private HashSet<PlayList> _playlists;

        public PlaylistRepository(Database database)
        {
            _playlists = database._playlists;
        }

        public void Add(PlayList playlist)
        {
            _playlists.Add(playlist);
        }

        public void AddMediaToPlaylist(PlayList playlist, Media media)
        {
            playlist.AddToList(media);
        }

        public void AddToUserPlaylist(User user, PlayList playlist)
        {
            user.AddPlaylist(playlist);
        }

        public IEnumerable<PlayList> GetAllPublicPlaylists()
        {
            return _playlists.Where(p => p.IsPrivate == false).ToList();
        }

        public Media? GetMediaInPlaylistById(Guid playlistId, Guid mediaId)
        {
            var playlist= _playlists.FirstOrDefault(p=>p.Id==playlistId);
            if(playlist is null){
                throw new ArgumentException($"Invalid argument. Playlist with{playlistId} is not found.");
            }
            return playlist.GetAllMediaItems().FirstOrDefault(i=>i.Id==mediaId);
        }

        public PlayList? GetPlayListById(Guid id)
        {
            return _playlists.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<PlayList> GetPlaylistsByOwnerId(Guid id)
        {
            return _playlists.Where(p=>p.OwnerId==id);
        }

        public PlayList? GetUserPlaylistById(Guid userId, Guid id)
        {
            return _playlists.FirstOrDefault(p=>p.OwnerId==userId&&p.Id==id);
        }

        public PlayList? GetUserPlaylistByName(Guid userId, string name)
        {
            return _playlists.FirstOrDefault(p=>p.OwnerId==userId&&p.Name.ToLower()==name.ToLower());
        }

        public void Remove(PlayList playlist)
        {
            _playlists.Remove(playlist);
        }

        public void RemoveFromUserPlaylist(User user, PlayList playlist)
        {
            user.DeletePlaylist(playlist);
        }

        public void RemoveMediaFromPlaylist(PlayList playlist, Media media)
        {
            playlist.RemoveFromList(media);
        }

        public void Update(PlayList playlist, string name)
        {
            playlist.Update(name);
        }
    }
}
