using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;

namespace MediaPlayer.Core.RepositoryAbstraction
{
    public interface IPlaylistRepository
    {
        public IEnumerable<PlayList> GetAllPublicPlaylists();
        public void AddNewPlaylist(PlayList playlist);
        public void RemovePlaylist(PlayList playlist);
        public IEnumerable<PlayList>? GetPlaylistByName(string name);
    }
}