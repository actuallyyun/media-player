using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Core.src.Entity
{
    public class User:BaseEntity 
    {
        public readonly string Username; // username must be unique and cannot be changed
        public string Email { get; set; }
        public string FullName { get; set; }
        public virtual UserType Type=>UserType.User; // Can be overridden by derived class Admin
        private HashSet<PlayList> _playlists;
        public readonly DateTime CreatedAt;
        public DateTime LastUpdatedAt;

        public User(string username, string email, string fullName)
        {
            Username = username;
            Email = email;
            FullName = fullName;
            _playlists=[];
            CreatedAt = DateTime.Now;
        }

        public void Update(string?email,string?fullName){
            if(email is not null) Email=email; 
            if(fullName is not null) FullName=fullName;
            LastUpdatedAt=DateTime.Now;
        }

        public IEnumerable<PlayList> GetPlaylist(){ // list cannot be modified
            return _playlists.ToList().AsReadOnly();
        }

        public void AddPlaylist(PlayList playlist)
        {
            _playlists.Add(playlist);
        }

        public void DeletePlaylist(PlayList playList)
        {
            _playlists.Remove(playList);
        }

        public override string ToString()
        {
            return $"Usename:{Username},email:{Email},fullname:{FullName},Type:{Type}";
        }
    }
}
