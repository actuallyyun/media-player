using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IPlaylistRepository
    {
        public IEnumerable<PlayList> GetAllPublicPlaylists();
        public PlayList? GetPlayListById(Guid id);
        public PlayList? GetUserPlaylistById(Guid userId, Guid id);
        public PlayList? GetUserPlaylistByName(Guid userId, string name);
        public Media? GetMediaInPlaylistById(Guid playlistId, Guid mediaId);
        public void Add(PlayList playlist);
        public void AddToUserPlaylist(User user, PlayList playlist);
        public void AddMediaToPlaylist(PlayList playlist, Media media);
        public void Remove(PlayList playlist);
        public void RemoveFromUserPlaylist(User user, PlayList playlist);
        public void RemoveMediaFromPlaylist(PlayList playlist, Media media);
        public void Update(PlayList playlist, string name);
    }
}
