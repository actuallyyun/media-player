using MediaPlayer.Core.src.Entity;
using MediaPlayer.Service.src.DTO;

namespace MediaPlayer.Service.src.Utils
{
    public class PlayListFactory
    {
        public PlayList Create(PlayListCreateDTO playListCreate){
            // TODO: check if the owner id exsists
            var isPrivate=playListCreate.IsPrivate??false;
            PlayList  newPlayList=new PlayList(playListCreate.OwnerId,playListCreate.Name,isPrivate);
            return newPlayList;
        }
    }
}