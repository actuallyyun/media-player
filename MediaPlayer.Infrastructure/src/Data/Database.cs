using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Infrastructure.src.Data
{
    public class Database
    {
        public HashSet<Media> _media;
        public HashSet<User> _users;
        public HashSet<PlayList> _playlists;
        public static Database _instance;
        private Database(){
           _media=SeedMedia();
           _users=[];
           _playlists=[];
        }

        public HashSet<Media> SeedMedia(){
            HashSet<Media> media=[];
            Media video1 = new Video("Sample Video 1", "John Doe", 2020);
            Media video2 = new Video("Sample Video 2", "Jane Smith", 2018);
            Media audio1 = new Audio("Sample Audio 1", "Michael Johnson", 2015);
            Media audio2 = new Audio("Sample Audio 2", "Emily Brown", 2019 );
            Media audio3 = new Audio("Sample Audio 3", "David Lee", 2021);
            media.Add(video1);
            media.Add(video2);
            media.Add(audio1);
            media.Add(audio2);
            media.Add(audio3);
            return media;
        }
        public static Database GetDatabase(){
            if(_instance is null){
                _instance=new Database();
            }
            return _instance;
        }
    }
}