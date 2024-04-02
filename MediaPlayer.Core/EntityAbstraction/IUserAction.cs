using MediaPlayer.Core.Entity;

namespace MediaPlayer.Core.EntityAbstraction
{
    public interface IUserAction
    {
        public PlayList CreateNewPlaylist(string name);
        public void AddPlaylist(PlayList playlist);
        public void DeletePlaylistById(int id);
        public void PlayPlaylist(PlayList playlist);
        public void PausePlaylist(PlayList playlist);
        public void StopPlaylist(PlayList playlist);

    }
}