namespace MediaPlayer.Core.src.Entity
{
    public class PlayList
    {
        public Guid OwnerId{get;}
        public string Name { get; set; }
        public HashSet<Media> _mediaItems{get;set;}
        public bool IsPrivate { get; set; }
        public bool IsPlaying{get;set;} // default to false
        public bool IsPaused{get;set;}
        public Guid Id{get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime LastUpdatedAt{get;set;}

        public PlayList(Guid ownerId, string name, bool isPrivate = false)
        { // default private setting is false. 
        // If it's private, it cannot be accessed by other users.
            OwnerId=ownerId;
            Name = name;
            IsPrivate = isPrivate;
            Id=Guid.NewGuid();
            IsPlaying=false;
            IsPaused=false;
            _mediaItems=[];
            CreatedAt = DateTime.Now;
        }

        public IEnumerable<Media> GetList(){
            return _mediaItems.ToList().AsReadOnly();
        }

        public void AddToList(Media media)
        {
            _mediaItems.Add(media);
            LastUpdatedAt=DateTime.Now;
        }

        public void RemoveFromList(Media media)
        {
            _mediaItems.Remove(media);
            LastUpdatedAt=DateTime.Now;
        }

        public void Update(string name){
            Name=name;
        }

        public Media? GetMediaById(Guid id){
            return _mediaItems.FirstOrDefault(m=>m.Id==id);
        }

        public override string ToString()
        {
            return $"PlayList Name:{Name},items:{_mediaItems.Count},Owner:{OwnerId}";
        }
    }
}
