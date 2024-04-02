namespace MediaPlayer.Core.Entity
{
    public class Media
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }

        public int Id;
        public Media(string title, string artist, int year)
        {
            Title = title;
            Artist = artist;
            Year = year;
            Id++;
        }
    }
}
