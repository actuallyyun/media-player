using MediaPlayer.Core.src.Utils;
using Microsoft.VisualBasic;
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Core.src.Entity
{
    public abstract class Media // mark it abstract so it cannot be instantiated
    {
        public abstract MediaType Type { get; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }

        public int Volumn { get; set; }

        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public DateTime LastUpdatedAt { get; set; }

        public Media(string title, string artist, int year)
        {
            Title = title;
            Artist = artist;
            if (!Validator.IsValidYear(year))
            {
                throw new ArgumentException("Invalid Year argument.");
            }

            Year = year;

            Volumn = 50; // set default vol
        }

        public void Update(string? title, string? artist, int? year)
        {
            if (title is not null)
                Title = title;
            if (artist is not null)
                Artist = artist;
            if (year is not null && Validator.IsValidYear(year))
            {
                Year = (int)year;
            }
        }

        public void SetVolumn(int vol)// all media can set volumn
        {
            Volumn = vol;
        }

        public override string ToString()
        {
            return $"Media Type:{Type},Title:{Title}, Artist:{Artist},Year:{Year}";
        }
    }
}
