using MediaPlayer.Core.EntityAbstraction;

namespace MediaPlayer.Core.Entity
{
    public class User:IUserAction
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public Guid Id;

        public HashSet<PlayList> _playlists;

        public User(string username, string email, string fullName)
        {
            Username = username;
            Email = email;
            FullName = fullName;
            Id=Guid.NewGuid();
        }

        public PlayList CreateNewPlaylist(string name)
        {
            throw new NotImplementedException();
        }

        public void AddPlaylist(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        public void DeletePlaylistById(int id)
        {
            throw new NotImplementedException();
        }

        public void PlayPlaylist(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        public void PausePlaylist(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        public void StopPlaylist(PlayList playlist)
        {
            throw new NotImplementedException();
        }
    }
}
