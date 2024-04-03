using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Infrastructure.src.Data
{
    public class Database
    {
        public HashSet<Media> _media;
        public HashSet<User> _users;
        public HashSet<PlayList> _playlists;
        public Database(){
           _media=[];
           _users=[];
           _playlists=[];
        }
    }
}