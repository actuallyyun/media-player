
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IPlaylistRepository
    {
        public IEnumerable<PlayList> GetAllPublicPlaylists();
        public void AddNewPlaylist(PlayList playlist);
        public void RemovePlaylist(PlayList playlist);
        public IEnumerable<PlayList>? GetPlaylistByName(string name);
    }
}