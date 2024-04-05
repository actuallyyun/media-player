
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IPlaylistRepository
    {
        public IEnumerable<PlayList> GetAllPublicPlaylists();
        public PlayList? GetPlayListById(Guid id);
        public PlayList? GetUserPlaylistById(Guid userId,Guid id);
        public PlayList?GetUserPlaylistByName(Guid userId,string name);
        public void Add(PlayList playlist);
        public void Remove(PlayList playlist);
        public void Update(PlayList playlist,string name);
    }
}