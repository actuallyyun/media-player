using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Service.src.DTO
{
    public class PlayListCreateDTO
    {
        public readonly Guid OwnerId;
        public string Name;
        public bool? IsPrivate { get; set; }

        public PlayListCreateDTO(Guid ownerId, string name, bool? isPrivate = false)
        {
            Console.WriteLine($"owner id to create:{ownerId}");
            // TODO could potentially not have ownerId here since userService will have it
            OwnerId = ownerId;
            Name = name;
            IsPrivate = isPrivate;
        }
    }
}
