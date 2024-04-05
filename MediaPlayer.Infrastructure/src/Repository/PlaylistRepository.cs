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

        public IEnumerable<PlayList> GetAllPublicPlaylists()
        {
            return _playlists.Where(p => p.IsPrivate == false).ToList();
        }

        public PlayList? GetPlayListById(Guid id)
        {
            return _playlists.FirstOrDefault(p => p.Id == id);
        }

        public void Remove(PlayList playlist)
        {
            _playlists.Remove(playlist);
        }

    }
}
