namespace MediaPlayer.Core.Entity
{
    public class PlayList
    {
        public string Name { get; set; }
        public HashSet<BaseMedia> _list;
        public bool IsPrivate { get; set; }

        public int Id;

        public PlayList(string name, bool isPrivate = false)
        { // default private setting is false
            Name = name;
            IsPrivate = isPrivate;
            Id++;
        }

        public void AddToList(BaseMedia media)
        {
            _list.Add(media);
        }

        public void RemoveFromList(BaseMedia media)
        {
            _list.Remove(media);
        }
    }
}
