using Microsoft.VisualBasic;

namespace MediaPlayer.Core.src.Entity
{
    public enum MediaType
    {
        Video,
        Audio
    }

    public abstract class Media // mark it abstract so it cannot be instantiated
    {
        public abstract MediaType Type { get;}
        public string Title ;
        public string Artist ;
        public int Year;

        public int Volumn;

        public Guid Id;
        public DateTime CreatedAt;
        public DateTime LastUpdatedAt;

        public Media(string title, string artist, int year)
        {
            Title = title;
            Artist = artist;
             if (year >= 1000 && year <= DateAndTime.Year(DateTime.Now))
                {
                    Year = year;
                }else{
                    Console.WriteLine("Invalid year");
                }
                Volumn=50;// set default vol
        }

        public void SetVolumn(int vol){
            Volumn=vol;
        }

        public override string ToString()
        {
            return $"Media Type:{Type},Title:{Title}, Artist:{Artist},Year:{Year}";
        }
    }
}
