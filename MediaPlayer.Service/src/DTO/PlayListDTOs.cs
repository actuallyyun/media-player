using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Service.src.DTO
{
    public class PlayListCreateDTO
    {
        public Guid OwnerId;
        public string Name ;
        public HashSet<Media> _mediaItems;
        public bool? IsPrivate { get; set; }

        public PlayListCreateDTO(Guid ownerId,string name, bool? isPrivate=false){ 
            // TODO could potentially not have ownerId here since userService will have it
            OwnerId=ownerId;
            Name=name;
            IsPrivate=isPrivate;
        }


    }
}