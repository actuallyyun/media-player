using MediaPlayer.Core.src.EntityAbstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class User 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public virtual bool IsAdmin=>false; // does not allow to change admin setting.

        public Guid Id;

        public HashSet<PlayList> _playlists;

        public DateTime CreatedAt;
        public DateTime LastUpdatedAt;

        public User(string username, string email, string fullName)
        {
            Username = username;
            Email = email;
            FullName = fullName;
            Id = Guid.NewGuid();
            _playlists=[];
            CreatedAt = DateTime.Now;
        }

        public void AddPlaylist(PlayList playlist)
        {
            _playlists.Add(playlist);
        }

        public void DeletePlaylistById(PlayList playList)
        {
            _playlists.Remove(playList);
        }

        public override string ToString()
        {
            return $"Usename:{Username},email:{Email},fullname:{FullName},isAdmin:{IsAdmin}";
        }
    }
}
