
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IPlaylistRepository
    {
        public IEnumerable<PlayList> GetAllPublicPlaylists();
        public PlayList? GetPlayListById(Guid id);
        public void Add(PlayList playlist);
        public void Remove(PlayList playlist);
    }
}