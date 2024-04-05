using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Infrastructure.src.Data
{
    public class Database
    {
        public HashSet<Media> _media;
        public HashSet<User> _users;
        public HashSet<PlayList> _playlists;
        public static Database _instance;

        private Database()
        {
            _media = SeedMedia();
            _users = SeedUser();
            _playlists = SeedPlaylist();
        }

        public HashSet<Media> SeedMedia()
        {
            HashSet<Media> media = [];
            Media video1 = new Video("Sample Video 1", "John Doe", 2020);
            Media video2 = new Video("Sample Video 2", "Jane Smith", 2018);
            Media audio1 = new Audio("Sample Audio 1", "Michael Johnson", 2015);
            Media audio2 = new Audio("Sample Audio 2", "Emily Brown", 2019);
            Media audio3 = new Audio("Sample Audio 3", "David Lee", 2021);
            media.Add(video1);
            media.Add(video2);
            media.Add(audio1);
            media.Add(audio2);
            media.Add(audio3);
            return media;
        }

        public HashSet<User> SeedUser()
        {
            HashSet<User> users = [];
            User user1 = new User("user1", "user1@example.com", "User One");
            User user2 = new User("user2", "user2@example.com", "User Two");
            User admin1 = new Admin("admin1", "admin1@example.com", "Admin One");
            User user3 = new User("user3", "user3@example.com", "User Three");
            User admin2 = new Admin("admin2", "admin2@example.com", "Admin Two");
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(admin1);
            users.Add(admin2);
            return users;
        }

        public HashSet<PlayList> SeedPlaylist()
        {
            HashSet<PlayList> playlists = new HashSet<PlayList>();
            foreach (User user in _users)
            {
                PlayList playlist = new PlayList(user.Id, "sample playlist", false);
                foreach (Media m in _media)
                {
                    playlist.AddToList(m);
                }

                playlists.Add(playlist);
            }
            return playlists;
        }

        public static Database GetDatabase()
        {
            if (_instance is null)
            {
                _instance = new Database();
            }
            return _instance;
        }
    }
}
