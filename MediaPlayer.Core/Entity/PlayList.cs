namespace MediaPlayer.Core.Entity
{
    public class PlayList
    {
        public string Name { get; set; }
        public HashSet<Media> _list;
        public bool IsPrivate { get; set; }

        public int Id;

        public DateTime CreatedAt;
        public DateTime LastUpdatedAt;

        public PlayList(string name, bool isPrivate = false)
        { // default private setting is false
            Name = name;
            IsPrivate = isPrivate;
            Id++;
            CreatedAt = DateTime.Now;
        }

        public void AddToList(Media media)
        {
            _list.Add(media);
        }

        public void RemoveFromList(Media media)
        {
            _list.Remove(media);
        }
    }
}
