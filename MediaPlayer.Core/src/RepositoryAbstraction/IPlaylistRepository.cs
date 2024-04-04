
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IPlaylistRepository
    {
        public IEnumerable<PlayList> GetAllPublicPlaylists();
        public HashSet<PlayList>GetPlayListsByOwnerId(Guid id);
        public PlayList? GetPlayListById(Guid id);
        public void AddPlaylist(PlayList playlist);
        public void RemovePlaylistById(Guid id);
        public void UpdatePlaylist(PlayList playlist);
        public IEnumerable<PlayList>? GetPlaylistByName(string name);
    }
}